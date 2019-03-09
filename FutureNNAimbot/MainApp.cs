namespace FutureNNAimbot
{
    public  class MainApp
    {

        private Settings settings;
        private GameProcess gp;
        private gController gc;

        public void Start()
        {
            settings = Settings.ReadSettings();
            var nNet = NeuralNet.Create(settings.Game);

            gp = GameProcess.Create(settings);
            gc = new gController(settings);

            if (nNet == null) {
                var ta = new TrainingApp(gp, gc, nNet);
                ta.startTrainingMode();
            }
            
            var ab = new Aimbot(settings, gp, gc, nNet);
            ab.Start();
        }
        
    }
}
