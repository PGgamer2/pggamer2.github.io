using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MC_Server_Manager
{
    public partial class Main : Form
    {
        public string versions;
        public JObject JobjVersions;
        public JArray JArrVersions;

        public Process cmd = new Process();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Get versions and put them in combobox
            versions = GetTextFromURL("https://launchermeta.mojang.com/mc/game/version_manifest.json");
            if (versions != null)
            {
                JobjVersions = JObject.Parse(versions);
                JArrVersions = (JArray)JobjVersions["versions"];
                for (int i = 0; i < JArrVersions.Count; i++)
                {
                    comboBoxVersions.Items.Add((string)JobjVersions["versions"][i]["id"]);
                }
                comboBoxVersions.SelectedIndex = 0;
                // CMD Process Info
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.OutputDataReceived += cmd_OutputDataReceived;
                cmd.Start();
                cmd.BeginOutputReadLine();

                cmd.StandardInput.WriteLine("cd \"" + Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\"");
                cmd.StandardInput.Flush();
            }
            else
            {
                DialogResult RestartDialogResult = MessageBox.Show("Click yes to restart and retry.", "Restart?", MessageBoxButtons.YesNo);
                if (RestartDialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (RestartDialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
        }

        public string GetTextFromURL(string gtfurl)
        {
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(gtfurl);
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch (WebException)
            {
                MessageBox.Show("An error has occurred!\nCannot connect to internet or to the mojang server.");
                return null;
            }
        }

        private void DLServerButton_Click(object sender, EventArgs e)
        {
            // Get server file URL and download
            for (int i = 0; i < JArrVersions.Count; i++)
            {
                if ((string)JobjVersions["versions"][i]["id"] == comboBoxVersions.Text)
                {
                    string VerINFO = GetTextFromURL((string)JobjVersions["versions"][i]["url"]);
                    if (VerINFO != null)
                    {
                        JObject JobjVerINFO = JObject.Parse(VerINFO);
                        if (JobjVerINFO["downloads"]["server"] != null)
                        {
                            startButton.Enabled = false;
                            formPanel.Enabled = false;
                            _ = wc_DownloadAsync((string)JobjVerINFO["downloads"]["server"]["url"]);
                        }
                        else
                        {
                            MessageBox.Show("An error has occurred!\nThis version hasn't a server file.");
                        }
                    }
                }
            }
        }

        async Task wc_DownloadAsync(string serverurl)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                await wc.DownloadFileTaskAsync(new Uri(serverurl), Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\server.jar");
            }
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Event to track the progress
            DLProgressBar.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100)
            {
                formPanel.Enabled = true;
                DLProgressBar.Value = 0;
                startButton.Enabled = true;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\server.jar"))
            {
                DLServerButton.Enabled = false;
                string startcmd = "java";
                if (textBoxRAM.Text != "" && textBoxRAM.Text != null)
                {
                    startcmd += " -Xms" + textBoxRAM.Text + "M -Xmx" + textBoxRAM.Text + "M";
                }
                startcmd += " -jar server.jar nogui";
                if (checkBoxFUpgrade.Checked)
                {
                    startcmd += " forceUpgrade";
                }
                cmd.StandardInput.WriteLine(startcmd);
                cmd.StandardInput.Flush();
            }
            else
            {
                MessageBox.Show("Put this program inside the server folder\nor download the .jar file with the \"Download Server\" button.");
            }
        }

        void cmd_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            if (InvokeRequired)
            {
                labelOutputConsole.Invoke(new MethodInvoker(delegate { labelOutputConsole.Text += e.Data + "\n"; }));
                if (e.Data.Length > 11)
                {
                    if (e.Data.Remove(0, 11) == "[main/INFO]: You need to agree to the EULA in order to run the server. Go to eula.txt for more info."
                        || e.Data.Remove(0, 11) == "[Server thread/INFO]: Stopping server")
                    {
                        DLServerButton.Invoke(new MethodInvoker(delegate { DLServerButton.Enabled = true; }));
                    }
                }
            }
            else
            {
                labelOutputConsole.Text += e.Data + "\n";
                if (e.Data.Length > 11)
                {
                    if (e.Data.Remove(0, 11) == "[main/INFO]: You need to agree to the EULA in order to run the server. Go to eula.txt for more info."
                        || e.Data.Remove(0, 11) == "[Server thread/INFO]: Stopping server")
                    {
                        DLServerButton.Enabled = true;
                    }
                }
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            cmd.StandardInput.WriteLine("stop");
            cmd.StandardInput.Flush();
            DLServerButton.Enabled = true;
        }

        private void labelOutputConsole_TextChanged(object sender, EventArgs e)
        {
            consolePanel.VerticalScroll.Value = consolePanel.VerticalScroll.Maximum;
        }

        private void enterCmdButton_Click(object sender, EventArgs e)
        {
            cmd.StandardInput.WriteLine(customPrompt.Text);
            cmd.StandardInput.Flush();
            if (customPrompt.Text == "stop")
            {
                DLServerButton.Enabled = true;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            cmd.StandardInput.WriteLine("stop");
            cmd.StandardInput.Flush();
            cmd.Close();
        }

        private void textBoxRAM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !Double.IsNaN(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
