using System.Threading;
using System.Windows.Forms;

namespace FutureNNAimbot
{
    public  class MainApp
    {

        private Settings settings;
        private GameProcess gp;
        private gController gc;
        private TrainingApp ta;

        public void Start()
        {
            settings = Settings.ReadSettings();
            var nNet = NeuralNet.Create(settings.Game);

            gp = GameProcess.Create(settings);
            gc = new gController(settings);
            var dh = new DrawHelper(settings);

            ta = new TrainingApp(gp, gc, nNet, dh);

            if (nNet == null) 
                ta.startTrainingMode();
            var ab = new Aimbot(settings, gp, gc, nNet, dh);

            bool b = true;

           
            System.Drawing.Point CenterScreen = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                
                while (true)
                {
                    if (Util.IsKeyToggled(Keys.Insert))
                    {
                        b = !b;
                        ab.Enabled = b;
                    }

                    if (Util.IsKeyToggled(Keys.PageUp))
                    {
                        settings.selectedObject = (settings.selectedObject + 1) % nNet.TrainingNames.Length;
                    }

                    if (Util.IsKeyToggled(Keys.PageDown))
                    {
                        settings.selectedObject = (settings.selectedObject - 1 + nNet.TrainingNames.Length) % nNet.TrainingNames.Length;
                    }

                    if (settings.CursorToCenter)
                        Cursor.Position = CenterScreen;

                    if (Util.IsKeyToggled(Keys.NumPad0))
                    {
                        settings.CursorToCenter = !settings.CursorToCenter;
                    }

                    if (b)
                        ab.ReadKeys();
                    else
                        ta.ReadInput();
                    Thread.Sleep(10);
                }
            }).Start();

            while (true)
            {
                if (b)
                    ab.Run();
                else
                    ta.Run();
            }
        }
        
    }
}
