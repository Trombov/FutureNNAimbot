using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace FutureNNAimbot
{
    public class GameProcess
    {
       public Settings s;

        public string ProcessName { get { return s.Game; } }

        private GameProcess(Settings settings)
        {
            s = settings;

        }

        public static  GameProcess Create(Settings settings)
        {
            var gp = new GameProcess(settings);
            while (gp.IsRunning() == false)
            {
                Console.Clear();
                MessageBox.Show($"You have not launched {gp.s.Game}...");
                Console.WriteLine($"Waiting for {gp.s.Game} to open. Press any key to retry!");
                Console.ReadLine();
            }
            
            return gp;
        }
        
        public bool IsRunning()
        {
            return Process.GetProcessesByName(s.Game).FirstOrDefault() != null;
        }
    }
}
