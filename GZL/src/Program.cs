using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace GZDoomLauncher
{
    static class Program
    {
        // GZLauncher made by PGgamer.
        // "GZDoom" and "Doom" aren't mine.
        // This software is not yours. Please appoint the author of the program if you modify it.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            String thisprocessname = Process.GetCurrentProcess().ProcessName;

            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
            {
                return;
            }
            else
            {
                Application.Run(new Main());
            }
        }
    }
}
