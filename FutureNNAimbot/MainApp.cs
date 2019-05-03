using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FutureNNAimbot
{
    public static class MainApp
    {

        static internal Settings settings;
        static internal NeuralNet nNet;
        static internal GameProcess gp;
        static internal DrawHelper dh;
        static internal gController gc;

        public static void Start()
        {
            settings = Settings.ReadSettings();
            nNet = NeuralNet.Create();
            gp = GameProcess.Create();
            dh = new DrawHelper();
            gc = new gController();

            if (nNet == null)
            {
                var ta = new TrainingApp();
                ta.startTrainingMode();
            }

            var ab = new Aimbot();
            ab.Start();
        }





    }
}
