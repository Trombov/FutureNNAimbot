namespace FutureNNAimbot
{
    public  class MainApp
    {

        private Settings settings;
        private GameProcess gp;

        public void Start()
        {
            settings = Settings.ReadSettings();
            var nNet = NeuralNet.Create(settings.Game);

            gp = GameProcess.Create(settings);


            if (nNet == null) {
                var ta = new TrainingApp(gp,nNet);
                ta.startTrainingMode();
            }
            
            var ab = new Aimbot(settings, gp, nNet);
            ab.Start();
        }
        
    }
}
