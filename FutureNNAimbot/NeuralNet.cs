using Alturos.Yolo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FutureNNAimbot
{
    public class NeuralNet
    {
        public YoloWrapper yoloWrapper;

        public string[] TrainingNames;

        public bool TrainingMode { get; set; } = false;

        private NeuralNet()
        {

        }

        static public YoloWrapper GetYolo(string Game)
        {

            if (File.Exists($"trainfiles/{Game}.cfg") && File.Exists($"trainfiles/{Game}.weights") && File.Exists($"trainfiles/{Game}.names"))
            {
                var yoloWrapper = new YoloWrapper($"trainfiles/{Game}.cfg", $"trainfiles/{Game}.weights", $"trainfiles/{Game}.names");
                Console.Clear();
                if (yoloWrapper.EnvironmentReport.CudaExists == false)
                {
                    Console.WriteLine("Install CUDA 10");
                    Process.GetCurrentProcess().Kill();
                }
                if (yoloWrapper.EnvironmentReport.CudnnExists == false)
                {
                    Console.WriteLine("Cudnn doesn't exist");
                    Process.GetCurrentProcess().Kill();
                }
                if (yoloWrapper.EnvironmentReport.MicrosoftVisualCPlusPlus2017RedistributableExists == false)
                {
                    Console.WriteLine("Install Microsoft Visual C++ 2017 Redistributable");
                    Process.GetCurrentProcess().Kill();
                }
                if (yoloWrapper.DetectionSystem.ToString() != "GPU")
                {
                    MessageBox.Show("No GPU card detected. Exiting...");
                    Process.GetCurrentProcess().Kill();
                }
                return yoloWrapper;
            }
            return null;
        }

        static public NeuralNet Create(string Game)
        {
            var nn = new NeuralNet
            {
                TrainingNames = null,
                yoloWrapper = GetYolo(Game)
            };

            if (nn.yoloWrapper == null)
                return null;

            nn.TrainingNames = File.ReadAllLines($"trainfiles/{Game}.names");
            
            return nn;
        }


        public IEnumerable<Alturos.Yolo.Model.YoloItem> GetItems(System.Drawing.Image img, double confidence = 0.4)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return yoloWrapper.Detect(ms.ToArray())
                    .Where(x => x.Confidence > confidence && TrainingNames.Contains(x.Type));
            }
        }


    }
}
