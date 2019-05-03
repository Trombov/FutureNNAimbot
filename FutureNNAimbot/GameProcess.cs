using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FutureNNAimbot
{
    internal class GameProcess
    {
        private Settings s;

        public string ProcessName { get { return s.Game; } }

        private GameProcess(Settings settings)
        {
            s = settings;

        }

        public static GameProcess Create()
        {
            Settings settings = MainApp.settings;
            var gp = new GameProcess(settings);
            var p = Process.GetProcessesByName(settings.Game).FirstOrDefault();
            if (p == null)
            {
                //MessageBox.Show($"You have not launched {gp.s.Game}...");
                //Process.GetCurrentProcess().Kill();
                while (gp.IsRunning() == false)
                {
                    Console.Clear();
                    Console.WriteLine($"Waiting for {gp.s.Game} to open. Press any enter to retry!");
                    Console.ReadLine();
                }
            }
            return gp;
        }



        public bool IsRunning()
        {
            return Process.GetProcessesByName(s.Game).FirstOrDefault() != null;
        }

        public bool LastForeground =false;
        public bool IsForeground()
        {
            var fg = Process.GetProcessesByName(s.Game).FirstOrDefault()?.MainWindowHandle == User32.GetForegroundWindow();
            if (LastForeground != fg)
                LastForeground = fg;
            return fg;
        }
    }
}
