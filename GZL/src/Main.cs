using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GZDoomLauncher
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        string GZpath = Directory.GetCurrentDirectory() + "\\GZDoom";
        string ZDpath = Directory.GetCurrentDirectory() + "\\ZDoom";
        string LZpath = Directory.GetCurrentDirectory() + "\\LZDoom";

        private void Main_Load(object sender, EventArgs e)
        {
            this.BoxSkillLvL.SelectedIndex = 2;
            this.ZDoomSelComboBox.SelectedIndex = 0;
            ReloadList();
            try
            {
                if (File.Exists(Application.StartupPath + "\\zdoom.zip"))
                    File.Delete(Application.StartupPath + "\\zdoom.zip");
                if (File.Exists(Application.StartupPath + "\\ZDGitInfo.txt"))
                    File.Delete(Application.StartupPath + "\\ZDGitInfo.txt");
            }
            catch { Console.WriteLine("Can't delete useless files..."); }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Funny messages before leaving.
            string[] ExitMessages = {
                "Please don't leave, there's more demons to toast!",
                "Let's beat it -- This is turning into a bloodbath!",
                "I wouldn't leave if I were you. Work is much worse.",
                "Don't leave yet -- There's a demon around that corner!",
                "Ya know, next time you come in here I'm gonna toast ya.",
                "Go ahead and leave. See if I care.",
                "Are you sure you want to quit this great game?",
                "You want to quit? Then, thou hast lost an eighth!",
                "Don't go now, there's a dimensional shambler waiting in the Explorer!",
                "Get outta here and go back to your boring programs.",
                "If I were your boss, I'd deathmatch ya in a minute!",
                "Look, bud. You leave now and you forfeit your body count!",
                "Just leave. When you come back, I'll be waiting with a bat.",
                "You're lucky I don't smack you for thinking about leaving.",
                "Are you sure you want to quit this great game?"
            };
            Random randMsg = new Random();
            int index = randMsg.Next(ExitMessages.Length);
            DialogResult exitDialog = MessageBox.Show($"{ExitMessages[index]}", "Do you want to exit?", MessageBoxButtons.YesNo);

            if (exitDialog == DialogResult.Yes)
                Application.ExitThread();
            else e.Cancel = true;
        }

        private void ReloadList()
        {
            // Reload list of WADS
            string iwadpath = Directory.GetCurrentDirectory() + "\\IWADS";
            string pwadpath = Directory.GetCurrentDirectory() + "\\PWADS";
            Directory.CreateDirectory(iwadpath);
            Directory.CreateDirectory(pwadpath);

            string[] gwad = Directory.GetFiles(iwadpath, "*.wad");
            string[] gpk3 = Directory.GetFiles(iwadpath, "*.pk3");
            string[] gzip = Directory.GetFiles(iwadpath, "*.zip");
            string[] gpak = Directory.GetFiles(iwadpath, "*.pak");
            string[] gpk7 = Directory.GetFiles(iwadpath, "*.pk7");
            string[] ggrp = Directory.GetFiles(iwadpath, "*.grp");
            string[] grff = Directory.GetFiles(iwadpath, "*.rff");
            string[] totalIWADS = gwad.Union(gpk3).Union(gzip).Union(gpak).Union(gpk7).Union(ggrp).Union(grff).ToArray();
            List<string> IWADStrList = new List<string>(totalIWADS);
            IWADlist.Items.Clear();
            foreach (string IWelem in IWADStrList)
            {
                IWADlist.Items.Add(new ListViewItem(IWelem));
            }

            string[] fwad = Directory.GetFiles(pwadpath, "*.wad");
            string[] fpk3 = Directory.GetFiles(pwadpath, "*.pk3");
            string[] fzip = Directory.GetFiles(pwadpath, "*.zip");
            string[] fpak = Directory.GetFiles(pwadpath, "*.pak");
            string[] fpk7 = Directory.GetFiles(pwadpath, "*.pk7");
            string[] fgrp = Directory.GetFiles(pwadpath, "*.grp");
            string[] frff = Directory.GetFiles(pwadpath, "*.rff");
            string[] fdeh = Directory.GetFiles(pwadpath, "*.deh");
            string[] fbex = Directory.GetFiles(pwadpath, "*.bex");
            string[] totalPWADS = fwad.Union(fpk3).Union(fzip).Union(fpak).Union(fpk7).Union(fgrp).Union(frff).Union(fdeh).Union(fbex).ToArray();
            List<string> PWADStrList = new List<string>(totalPWADS);
            PWADlist.Items.Clear();
            foreach (string PWelem in PWADStrList)
            {
                PWADlist.Items.Add(new ListViewItem(PWelem));
            }
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            ReloadList();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            string ZDName = "GZDoom";
            string ZDDir = GZpath;
            string ZDEXEPath = GZpath + "\\gzdoom.exe";
            switch (ZDoomSelComboBox.SelectedIndex)
            {
                case 1:
                    ZDName = "LZDoom";
                    ZDDir = LZpath;
                    ZDEXEPath = LZpath + "\\lzdoom.exe";
                    break;
                case 2:
                    ZDName = "ZDoom";
                    ZDDir = ZDpath;
                    ZDEXEPath = ZDpath + "\\zdoom.exe";
                    break;
            }

            string totalArguments = "";
            Directory.CreateDirectory(ZDDir);
            if (File.Exists(ZDEXEPath))
            {
                if (IWADlist.SelectedItems.Count > 0)
                {
                    // Set Parameters
                    totalArguments = " -iwad \"" + IWADlist.SelectedItems[0].Text + "\"";

                    if (PWADlist.SelectedItems.Count > 0)
                    {
                        string selectedPWADS = "";
                        for (int i = 0; i < PWADlist.Items.Count; i++)
                        {
                            if (PWADlist.Items[i].Selected)
                            {
                                selectedPWADS += " \"" + PWADlist.Items[i].Text + "\"";
                            }
                        }
                        totalArguments += " -file" + selectedPWADS;
                    }

                    if (radioNoSounds.Checked)
                    {
                        totalArguments += " -nosound";
                    }
                    if (radioNoSFX.Checked)
                    {
                        totalArguments += " -nosfx";
                    }
                    if (radioNoMusic.Checked)
                    {
                        totalArguments += " -nomusic";
                    }

                    if (StartFromBox.TextLength > 0 && StartFromBox.Text != " ")
                    {
                        totalArguments += " +map " + StartFromBox.Text;
                    }

                    if (BoxSkillLvL.Text != "Hurt me plenty")
                    {
                        string CurrentSkillLvL = "3";
                        switch (BoxSkillLvL.Text)
                        {
                            case "I'm too young to die":
                                CurrentSkillLvL = "1";
                                break;
                            case "Hey, not too rough":
                                CurrentSkillLvL = "2";
                                break;
                            case "Ultra-Violence":
                                CurrentSkillLvL = "4";
                                break;
                            case "Nightmare!":
                                CurrentSkillLvL = "5";
                                break;
                        }
                        totalArguments += " -skill " + CurrentSkillLvL;
                    }

                    if (NoMonstersButton.Checked)
                    {
                        totalArguments += " -nomonsters";
                    }

                    if (FastMonstersButton.Checked)
                    {
                        totalArguments += " -fast";
                    }

                    if (MonstersRespawnButton.Checked)
                    {
                        totalArguments += " -respawn";
                    }

                    if (ConfigPathBox.TextLength > 0 && ConfigPathBox.Text != " ")
                    {
                        totalArguments += " -config \"" + ConfigPathBox.Text + "\"";
                    }

                    if (HostNumber.Value > 0 && HostNumber.Enabled)
                    {
                        totalArguments += " -host " + HostNumber.Value;
                    }

                    string JoinPort = "";
                    if (PortBox.Text != "5029" && PortBox.TextLength > 0 && PortBox.Text != " " && PortBox.Enabled)
                    {
                        if (ConnectBox.TextLength > 0)
                        {
                            JoinPort = PortBox.Text;
                        }
                        else
                        {
                            totalArguments += " -port " + PortBox.Text;
                        }
                    }

                    if (ConnectBox.TextLength > 0 && ConnectBox.Enabled)
                    {
                        if (PortBox.Text != "5029" && PortBox.TextLength > 0 && PortBox.Text != " ")
                        {
                            totalArguments += " -join " + ConnectBox.Text + ":" + JoinPort;
                        }
                        else
                        {
                            totalArguments += " -join " + ConnectBox.Text;
                        }
                    }

                    if (DeathmatchButton.Checked && DeathmatchButton.Enabled)
                    {
                        totalArguments += " -deathmatch";
                    }

                    if (PacketServerButton.Checked && PacketServerButton.Enabled)
                    {
                        totalArguments += " -netmode 1";
                    }

                    if (CustomParamBox.TextLength > 0 && CustomParamBox.Text != " ")
                    {
                        totalArguments += " " + CustomParamBox.Text;
                    }

                    // Start GZDoom
                    Process GZInfo = new Process();
                    GZInfo.StartInfo.FileName = ZDEXEPath;
                    GZInfo.StartInfo.Arguments = totalArguments;
                    GZInfo.StartInfo.UseShellExecute = false;
                    GZInfo.Start();
                    if (CloseOnStartButton.Checked)
                    {
                        Application.ExitThread();
                    }
                }
                else   // If something is missing then...
                {
                    MessageBox.Show("Select an IWAD first!", "GZDoom Launcher");
                }
            }
            else
            {
                DialogResult MissGZMsg = MessageBox.Show($"{ZDName} is missing.\nWould you like to download it?", "Some files are missing...", MessageBoxButtons.YesNo);
                if (MissGZMsg == DialogResult.Yes)
                {
                    GZDL Downloader = new GZDL();
                    Downloader.ZDTypeDL = ZDoomSelComboBox.SelectedIndex;
                    Downloader.ShowDialog();
                }
            }
        }

        private void ConfigExplorerSelectButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog SelectConfigFileDialog = new OpenFileDialog();
            SelectConfigFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            SelectConfigFileDialog.DefaultExt = "ini";
            SelectConfigFileDialog.Filter = "Configuration files (*.ini)|*.ini|All files (*.*)|*.*";
            SelectConfigFileDialog.ShowDialog();
            ConfigPathBox.Text = SelectConfigFileDialog.FileName;
        }

        private void HostNumber_ValueChanged(object sender, EventArgs e)
        {
            if (HostNumber.Value > 0)
            {
                PortBox.Enabled = true;
                DeathmatchButton.Enabled = true;
                PacketServerButton.Enabled = true;
                ConnectBox.Enabled = false;
            }
            else
            {
                PortBox.Enabled = false;
                DeathmatchButton.Enabled = false;
                PacketServerButton.Enabled = false;
                ConnectBox.Enabled = true;
            }
        }

        private void ConnectBox_TextChanged(object sender, EventArgs e)
        {
            if (ConnectBox.TextLength > 0)
            {
                PortBox.Enabled = true;
                HostNumber.Enabled = false;
            }
            else
            {
                PortBox.Enabled = false;
                HostNumber.Enabled = true;
            }
        }

        private void AddIWADButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog AddIWADialog = new OpenFileDialog();
            AddIWADialog.InitialDirectory = Directory.GetCurrentDirectory();
            AddIWADialog.DefaultExt = "wad";
            AddIWADialog.Filter = "Where's All the Data? (*.wad)|*.wad|PK3 Files (*.pk3)|*.pk3|Zipped Files (*.zip)|*.zip|PAK Files (*.pak)|*.pak|PK7 Files (*.pk7)|*.pk7|GRP Files (*.grp)|*.grp|RFF Files (*.rff)|*.rff|All files (*.*)|*.*";
            AddIWADialog.ShowDialog();
            if (AddIWADialog.FileName != "")
            {
                DialogResult CopyToIWADPrompt = MessageBox.Show("Would you like to copy this file to the IWAD directory?", "GZDoom Launcher", MessageBoxButtons.YesNo);
                if (CopyToIWADPrompt == DialogResult.Yes)
                {
                    try
                    {
                        File.Copy(AddIWADialog.FileName, Directory.GetCurrentDirectory() + "\\IWADS\\" + Path.GetFileName(AddIWADialog.FileName));
                    }
                    catch { MessageBox.Show("Cannot copy the file to the directory.", "GZDoom Launcher"); }
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\IWADS\\" + Path.GetFileName(AddIWADialog.FileName))) { IWADlist.Items.Add(new ListViewItem(Directory.GetCurrentDirectory() + "\\IWADS\\" + Path.GetFileName(AddIWADialog.FileName))); }
                }
                else
                {
                    IWADlist.Items.Add(new ListViewItem(AddIWADialog.FileName));
                }
            }
        }

        private void AddPWADButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog AddPWADialog = new OpenFileDialog();
            AddPWADialog.InitialDirectory = Directory.GetCurrentDirectory();
            AddPWADialog.DefaultExt = "wad";
            AddPWADialog.Filter = "Where's All the Data? (*.wad)|*.wad|PK3 Files (*.pk3)|*.pk3|Zipped Files (*.zip)|*.zip|PAK Files (*.pak)|*.pak|PK7 Files (*.pk7)|*.pk7|GRP Files (*.grp)|*.grp|RFF Files (*.rff)|*.rff|DeHackEd Files (*.deh)|*.deh|BOOM Extension Files (*.bex)|*.bex|All files (*.*)|*.*";
            AddPWADialog.ShowDialog();
            if (AddPWADialog.FileName != "")
            {
                DialogResult CopyToPWADPrompt = MessageBox.Show("Would you like to copy this file to the PWAD directory?", "GZDoom Launcher", MessageBoxButtons.YesNo);
                if (CopyToPWADPrompt == DialogResult.Yes)
                {
                    try
                    {
                        File.Copy(AddPWADialog.FileName, Directory.GetCurrentDirectory() + "\\PWADS\\" + Path.GetFileName(AddPWADialog.FileName));
                    }
                    catch { MessageBox.Show("Cannot copy the file to the directory.", "GZDoom Launcher"); }
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\PWADS\\" + Path.GetFileName(AddPWADialog.FileName))) { PWADlist.Items.Add(new ListViewItem(Directory.GetCurrentDirectory() + "\\PWADS\\" + Path.GetFileName(AddPWADialog.FileName))); }
                }
                else
                {
                    PWADlist.Items.Add(new ListViewItem(AddPWADialog.FileName));
                }
            }
        }

        private void SaveProfileButton_Click(object sender, EventArgs e)
        {
            string FinalJSON = "{\n";
            if (IWADlist.SelectedItems.Count > 0) FinalJSON += "\"SelectedIWAD\": \"" + IWADlist.SelectedItems[0].Text + "\",\n";
            if (PWADlist.SelectedItems.Count > 0)
            {
                FinalJSON += "\"SelectedPWADs\": [";
                for (int i = 0; i < PWADlist.Items.Count; i++)
                {
                    if (PWADlist.Items[i].Selected)
                        FinalJSON += " \"" + PWADlist.Items[i].Text + "\",";
                }
                FinalJSON = FinalJSON.Remove(FinalJSON.Length - 1);
                FinalJSON += " ],\n";
            }

            string JAudioSetting = "enabled";
            if (radioNoSounds.Checked)
                JAudioSetting = "nosound";

            if (radioNoSFX.Checked)
                JAudioSetting = "nosfx";

            if (radioNoMusic.Checked)
                JAudioSetting = "nomusic";

            FinalJSON += "\"Audio\": \"" + JAudioSetting + "\",\n";

            if (StartFromBox.TextLength > 0 && StartFromBox.Text != " ")
                FinalJSON += "\"StartMap\": \"" + StartFromBox.Text + "\",\n";

            if (BoxSkillLvL.Text != "Hurt me plenty")
            {
                string JCurrentSkillLvL = "3";
                switch (BoxSkillLvL.Text)
                {
                    case "I'm too young to die":
                        JCurrentSkillLvL = "1";
                        break;
                    case "Hey, not too rough":
                        JCurrentSkillLvL = "2";
                        break;
                    case "Ultra-Violence":
                        JCurrentSkillLvL = "4";
                        break;
                    case "Nightmare!":
                        JCurrentSkillLvL = "5";
                        break;
                }

                FinalJSON += "\"Skill\": " + JCurrentSkillLvL + ",\n";
            }

            if (NoMonstersButton.Checked)
                FinalJSON += "\"NoMonsters\": \"True\",\n";

            if (FastMonstersButton.Checked)
                FinalJSON += "\"FastMonsters\": \"True\",\n";

            if (MonstersRespawnButton.Checked)
                FinalJSON += "\"MonstersCanRespawn\": \"True\",\n";

            if (ConfigPathBox.TextLength > 0 && ConfigPathBox.Text != " ")
                FinalJSON += "\"ConfigFilePath\": \"" + ConfigPathBox.Text + "\",\n";

            if (HostNumber.Value > 0 && HostNumber.Enabled)
                FinalJSON += "\"Host\": " + HostNumber.Value + ",\n";

            if (PortBox.Text != "5029" && PortBox.TextLength > 0 && PortBox.Text != " " && PortBox.Enabled)
                FinalJSON += "\"Port\": \"" + PortBox.Text + "\",\n";

            if (ConnectBox.TextLength > 0 && ConnectBox.Enabled)
                FinalJSON += "\"ConnectTo\": \"" + ConnectBox.Text + "\",\n";

            if (DeathmatchButton.Checked && DeathmatchButton.Enabled)
                FinalJSON += "\"Deathmatch\": \"True\",\n";

            if (PacketServerButton.Checked && PacketServerButton.Enabled)
                FinalJSON += "\"PacketServer\": \"True\",\n";

            if (CustomParamBox.TextLength > 0 && CustomParamBox.Text != " ")
                FinalJSON += "\"OtherParameters\": \"" + CustomParamBox.Text + "\",\n";

            if (FinalJSON != "{\n") FinalJSON = FinalJSON.Remove(FinalJSON.Length - 2);
            FinalJSON += "\n}";
            SaveFileDialog JSONPath = new SaveFileDialog();
            JSONPath.InitialDirectory = Directory.GetCurrentDirectory();
            JSONPath.FileName = "profile.gzlp";
            JSONPath.DefaultExt = "gzlp";
            JSONPath.Filter = "GZLauncher Profile (*.gzlp)|*.gzlp|JavaScript Object Notation (*.json)|*.json";
            if (JSONPath.FileName.Length > 0 && JSONPath.ShowDialog() == DialogResult.OK)
                File.WriteAllText(JSONPath.FileName, FinalJSON);
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            InfoScreen AboutScreen = new InfoScreen();
            AboutScreen.ShowDialog();
        }

        private void LoadProfileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ProfilePath = new OpenFileDialog();
            ProfilePath.InitialDirectory = Directory.GetCurrentDirectory();
            ProfilePath.DefaultExt = "gzlp";
            ProfilePath.Filter = "GZLauncher Profile (*.gzlp)|*.gzlp|JavaScript Object Notation (*.json)|*.json|All files (*.*)|*.*";
            ProfilePath.ShowDialog();
            if (ProfilePath.FileName.Length > 0)
                LoadProfile(ProfilePath.FileName);
        }

        public void LoadProfile(string ArgProfilePath)
        {
            if (File.Exists(ArgProfilePath))
            {
                StreamReader reader = new StreamReader(ArgProfilePath);
                string PFJson = reader.ReadToEnd();
                PFJson = PFJson.Replace("\\", "\\\\");
                JObject Jobj = JObject.Parse(PFJson);

                if ((string)Jobj["Host"] != null && (string)Jobj["ConnectTo"] != null)
                {
                    MessageBox.Show("This file has both \"Host\" and \"ConnectTo\" values", "An error has occurred");
                }
                else
                {
                    if ((string)Jobj["SelectedIWAD"] != null)
                    {
                        if (IWADlist.FindItemWithText((string)Jobj["SelectedIWAD"]) == null)
                            IWADlist.Items.Add((string)Jobj["SelectedIWAD"]);
                    }

                    if ((JArray)Jobj["SelectedPWADs"] != null)
                    {
                        JArray JSelPWADsItems = (JArray)Jobj["SelectedPWADs"];
                        for (int i = 0; i < JSelPWADsItems.Count; i++)
                        {
                            if (PWADlist.FindItemWithText((string)Jobj["SelectedPWADs"][i]) == null)
                                PWADlist.Items.Add((string)Jobj["SelectedPWADs"][i]);
                        }
                    }

                    if ((string)Jobj["Audio"] != null)
                    {
                        if ((string)Jobj["Audio"] == "nosound")
                        {
                            radioNoSounds.Checked = true;
                        }
                        else if ((string)Jobj["Audio"] == "nosfx")
                        {
                            radioNoSFX.Checked = true;
                        }
                        else if ((string)Jobj["Audio"] == "nomusic")
                        {
                            radioNoMusic.Checked = true;
                        }
                        else
                        {
                            radioAllSounds.Checked = true;
                        }
                    }

                    if ((string)Jobj["StartMap"] != null)
                    {
                        StartFromBox.Text = (string)Jobj["StartMap"];
                    }

                    if ((string)Jobj["Skill"] != null)
                    {
                        BoxSkillLvL.SelectedIndex = 2;
                        switch ((string)Jobj["Skill"])
                        {
                            case "1":
                                BoxSkillLvL.SelectedIndex = 0;
                                break;
                            case "2":
                                BoxSkillLvL.SelectedIndex = 1;
                                break;
                            case "4":
                                BoxSkillLvL.SelectedIndex = 3;
                                break;
                            case "5":
                                BoxSkillLvL.SelectedIndex = 4;
                                break;
                        }
                    }

                    if ((string)Jobj["NoMonsters"] != null)
                    {
                        if ((string)Jobj["NoMonsters"] == "True")
                            NoMonstersButton.Checked = true;
                        else
                            NoMonstersButton.Checked = false;
                    }

                    if ((string)Jobj["FastMonsters"] != null)
                    {
                        if ((string)Jobj["FastMonsters"] == "True")
                            FastMonstersButton.Checked = true;
                        else
                            FastMonstersButton.Checked = false;
                    }

                    if ((string)Jobj["MonstersCanRespawn"] != null)
                    {
                        if ((string)Jobj["MonstersCanRespawn"] == "True")
                            MonstersRespawnButton.Checked = true;
                        else
                            MonstersRespawnButton.Checked = false;
                    }

                    if ((string)Jobj["ConfigFilePath"] != null)
                    {
                        ConfigPathBox.Text = (string)Jobj["ConfigFilePath"];
                    }

                    if ((string)Jobj["Host"] != null)
                    {
                        PortBox.Enabled = true;
                        DeathmatchButton.Enabled = true;
                        PacketServerButton.Enabled = true;
                        ConnectBox.Enabled = false;
                        HostNumber.Enabled = true;
                        HostNumber.Value = (decimal)Jobj["Host"];
                    }
                    else if ((string)Jobj["ConnectTo"] != null)
                    {
                        PortBox.Enabled = true;
                        HostNumber.Enabled = false;
                        ConnectBox.Enabled = true;
                        DeathmatchButton.Enabled = false;
                        PacketServerButton.Enabled = false;
                        ConnectBox.Text = (string)Jobj["ConnectTo"];
                    }

                    if ((string)Jobj["Port"] != null)
                    {
                        PortBox.Text = (string)Jobj["Port"];
                    }

                    if ((string)Jobj["Deathmatch"] != null)
                    {
                        if ((string)Jobj["Deathmatch"] == "True")
                        {
                            DeathmatchButton.Checked = true;
                        }
                        else
                        {
                            DeathmatchButton.Checked = false;
                        }
                    }

                    if ((string)Jobj["PacketServer"] != null)
                    {
                        if ((string)Jobj["PacketServer"] == "True")
                        {
                            PacketServerButton.Checked = true;
                        }
                        else
                        {
                            PacketServerButton.Checked = false;
                        }
                    }

                    if ((string)Jobj["OtherParameters"] != null)
                    {
                        CustomParamBox.Text = (string)Jobj["OtherParameters"];
                    }
                }
            }
        }
    }
}
