using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace GZDoomLauncher
{
    public partial class GZDL : Form
    {
        public GZDL()
        {
            InitializeComponent();
        }

        WebClient client;

        private void GZDL_Load(object sender, EventArgs e)
        {
            client = new WebClient();

            try { client.OpenRead("http://google.com/generate_204"); }
            catch
            {
                MessageBox.Show("You need internet connection to download GZDoom.\nRestart the software to continue.", "GZDoom Launcher");
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        Environment.Exit(1);
                    }));
                }
                else
                {
                    Environment.Exit(1);
                }
            }

            client.Headers.Add("user-agent", "Anything");
            client.DownloadFile("https://api.github.com/repos/coelckers/gzdoom/releases/latest", Application.StartupPath + "\\GZLatestLink.txt");
            StreamReader reader = new StreamReader(Application.StartupPath + "\\GZLatestLink.txt");
            string GZDGitAPI = reader.ReadToEnd();
            JObject obj = JObject.Parse(GZDGitAPI);
            string GZDlvg = (string)obj["tag_name"];
            string GZDlatestVersion = GZDlvg.Replace("g", "");
            string url = "https://github.com/coelckers/gzdoom/releases/download/g" + GZDlatestVersion + "/gzdoom-" + GZDlatestVersion.Replace(".", "-") + "-Windows-32bit.zip";

            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            if (Environment.Is64BitOperatingSystem)
            {
                url = url.Replace("32bit", "64bit");
            }
            Thread thread = new Thread(() =>
            {
                Uri uri = new Uri(url);
                client.DownloadFileAsync(uri, Application.StartupPath + "\\gzdoom.zip");
            });
            thread.Start();
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try { client.OpenRead("http://google.com/generate_204"); }
            catch
            {
                MessageBox.Show("You need internet connection to download GZDoom.\nRestart the software to continue.", "GZDoom Launcher");
                try
                {
                    if (File.Exists(Application.StartupPath + "\\gzdoom.zip"))
                    {
                        File.Delete(Application.StartupPath + "\\gzdoom.zip");
                    }
                    if (File.Exists(Application.StartupPath + "\\GZLatestLink.txt"))
                    {
                        File.Delete(Application.StartupPath + "\\GZLatestLink.txt");
                    }
                }
                catch { Console.WriteLine("Can't delete useless files..."); }
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        Environment.Exit(1);
                    }));
                }
                else
                {
                    Environment.Exit(1);
                }
            }

            if (File.Exists(Application.StartupPath + "\\gzdoom.zip"))
            {
                var zipFileName = Application.StartupPath + "\\gzdoom.zip";
                var targetDir = Application.StartupPath + "\\GZDoom";
                FastZip fastZip = new FastZip();
                string fileFilter = null;
                fastZip.ExtractZip(zipFileName, targetDir, fileFilter);
                MessageBox.Show("GZDoom successfully downloaded.\nRestart the software to complete the installation.", "GZDoom Launcher");
            }
            else
            {
                MessageBox.Show("An error has occurred.\nRestart the software to continue.", "GZDoom Launcher");
            }
            try
            {
                if (File.Exists(Application.StartupPath + "\\gzdoom.zip"))
                {
                    File.Delete(Application.StartupPath + "\\gzdoom.zip");
                }
                if (File.Exists(Application.StartupPath + "\\GZLatestLink.txt"))
                {
                    File.Delete(Application.StartupPath + "\\GZLatestLink.txt");
                }
            }
            catch { Console.WriteLine("Can't delete useless files..."); }
            if (InvokeRequired) {
                Invoke(new MethodInvoker(delegate () {
                    Environment.Exit(0);
                }));
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    progressBar.Minimum = 0;
                    double receive = double.Parse(e.BytesReceived.ToString());
                    double total = double.Parse(e.TotalBytesToReceive.ToString());
                    double percentage = receive / total * 100;
                    DTextProgressStatus.Text = $"Downloaded {string.Format("{0:0.##}", percentage)}%";
                    progressBar.Value = int.Parse(Math.Truncate(percentage).ToString());
                }));
            }
            catch
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        Environment.Exit(1);
                    }));
                }
                else
                {
                    Environment.Exit(1);
                }
            }
        }
    }
}
