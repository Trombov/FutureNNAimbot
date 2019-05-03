using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FutureNNAimbot
{
    internal static class MainApp
    {

        static internal Settings settings;
        static internal NeuralNet nNet;
        static internal GameProcess gameProcess;
        static internal DrawHelper drawHelper;
        static internal GController gameController;

        public static void Start()
        {
            settings = Settings.ReadSettings();
            nNet = NeuralNet.Create();
            gameProcess = GameProcess.Create();
            drawHelper = new DrawHelper();
            gameController = new GController();

            if (nNet == null)
            {
                var ta = new TrainingApp();
                ta.StartTrainingMode();
            }

            var ab = new Aimbot();
            ab.Start();
        }





    }
}
