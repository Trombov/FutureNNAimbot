using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FutureNNAimbot
{
    [DataContract]
    public class Settings
    {
        [DataMember]
        public int SizeX { get; set; } = 320;
        [DataMember]
        public int SizeY { get; set; } = 320;
        [DataMember]
        public string Game { get; set; } = "game";
        [DataMember]
        public bool SimpleRCS { get; set; } = true;
        [DataMember]
        public int SimpleRCSvalue { get; set; } = 2;
        [DataMember]
        public Keys ShootKey { get; set; } = Keys.MButton;
        [DataMember]
        public Keys TrainModeKey { get; set; } = Keys.Insert;
        [DataMember]
        public Keys ScreenshotKey { get; set; } = Keys.Home;
        [DataMember]
        public Keys ScreenshotModeKey { get; set; } = Keys.NumPad9;
        [DataMember]
        public float SmoothAim { get; set; } = 0;
        [DataMember]
        public bool Information { get; set; } = true;
        [DataMember]
        public bool Head { get; set; } = false;
        [DataMember]
        public bool DrawAreaRectangle { get; set; } = true;
        [DataMember]
        public bool DrawText { get; set; } = true;
        [DataMember]
        public int AutoAimDelayMs { get; set; } = 500;


        static public Settings ReadSettings()
        {
            // Read settings
            DataContractJsonSerializer Settings = new DataContractJsonSerializer(typeof(Settings[]));
            Settings[] settings = null;
            Settings auto_config = new Settings();
            using (var fs = new System.IO.FileStream("config.json", System.IO.FileMode.OpenOrCreate))
            {
                if (fs.Length == 0)
                {
                    Settings.WriteObject(fs, new Settings[1] { auto_config });
                    MessageBox.Show($"Created auto-config, change whatever settings you want and restart.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                    return null;
                }
                else settings = (Settings[])Settings.ReadObject(fs);
                return settings?[0];
            }
        }
    }
}
