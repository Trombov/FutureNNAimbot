using System.Threading;

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
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                
                while (true)
                {
                    if (Util.IsKeyToggled(System.Windows.Forms.Keys.Insert))
                    {
                        b = !b;
                        ab.Enabled = b;
                    }

                    if (Util.IsKeyToggled(System.Windows.Forms.Keys.PageUp))
                    {
                        settings.selectedObject = (settings.selectedObject + 1) % nNet.TrainingNames.Length;
                    }

                    if (Util.IsKeyToggled(System.Windows.Forms.Keys.PageDown))
                    {
                        settings.selectedObject = (settings.selectedObject - 1 + nNet.TrainingNames.Length) % nNet.TrainingNames.Length;
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
