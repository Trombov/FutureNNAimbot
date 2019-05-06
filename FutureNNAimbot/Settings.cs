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
    /// <summary>
    /// Settings class
    /// </summary>
    [DataContract]
    public class Settings
    {
        /// <summary>
        /// Size of window width
        /// </summary>
        [DataMember]
        public int SizeX { get; set; } = 320;
        /// <summary>
        /// Size of window height
        /// </summary>
        [DataMember]
        public int SizeY { get; set; } = 320;
        /// <summary>
        /// Filename of the game's EXE
        /// </summary>
        [DataMember]
        public string Game { get; set; } = "r5apex";
        /// <summary>
        /// Is Simple Recoil Control System Enabled
        /// </summary>
        [DataMember]
        public bool SimpleRCS { get; set; } = true;
        /// <summary>
        /// Ammount of pixels to move down after shooting to reduce recoil
        /// </summary>
        [DataMember]
        public int SimpleRCSvalue { get; set; } = 3;
        /// <summary>
        /// Key to Activate Train Mode
        /// </summary>
        [DataMember]
        public Keys TrainModeKey { get; set; } = Keys.Insert;
        /// <summary>
        /// Key to take a screenshot while in Train Mode
        /// </summary>
        [DataMember]
        public Keys ScreenshotKey { get; set; } = Keys.Home;
        /// <summary>
        /// Key to enable screenshot mode in training mode (use mouse to take screenshot)
        /// </summary>
        [DataMember]
        public Keys ScreenshotModeKey { get; set; } = Keys.NumPad9;
        /// <summary>
        /// Value to make a delay when snapping to target making a smooth mouse moving effect
        /// </summary>
        [DataMember]
        public float SmoothAim { get; set; } = 0;
        /// <summary>
        /// Snap automatically to head position else it will snap to center of enemy's mass.
        /// May cause missing shots due to unhandled recoil for far away targets.
        /// </summary>
        [DataMember]
        public bool Head { get; set; } = false;
        /// <summary>
        /// Draw the rectangle of the screen portion where aim bot is running
        /// </summary>
        [DataMember]
        public bool DrawAreaRectangle { get; set; } = false;
        /// <summary>
        /// Capture based on mouse position or screen center position
        /// </summary>
        [DataMember]
        public bool FollowMouse { get; set; } = false;
        /// <summary>
        /// Draw text in the upper left
        /// </summary>
        [DataMember]
        public bool DrawText { get; set; } = true;
        /// <summary>
        /// Draw opacity value for rectangle and enemy outline
        /// </summary>
        [DataMember]
        public byte DrawOpacity { get; set; } = 120;
        /// <summary>
        /// Ammount of miliseconds to keep tracking enemy after shooting key has been released (useful for semi automatic pistols or shotguns)
        /// </summary>
        [DataMember]
        public int AutoAimDelayMs { get; set; } = 250;
        /// <summary>
        /// Keys that will trigger aimbot snapping into target while holded down
        /// </summary>
        [DataMember]
        public IEnumerable<Keys> ShootKeys { get; set; } = new[] { Keys.LButton, Keys.RButton };
        /// <summary>
        /// Key that will pause aimbot snapping and resume after Shoot key is pressed again (useful for granade throwing)
        /// </summary>
        [DataMember]
        public Keys PauseKey { get; set; } = Keys.G;
        /// <summary>
        /// Key that will disable/enable aimbot
        /// </summary>
        [DataMember]
        public Keys DisableKey { get; set; } = Keys.F2;


        static internal Settings ReadSettings()
        {
            // Read settings
            var knowTypes = new List<Type> { typeof(EnumSurrogate) };
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Settings),
                knowTypes, int.MaxValue, false, new EnumeratorContractSurrogate(), false);

            var defaultSettings = new Settings();
            if (System.IO.File.Exists("config.json") == false)
            {
                saveFile(serializer, defaultSettings);
                MessageBox.Show($"Created auto-config, change whatever settings you want and restart.");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                return null;
            }
            Settings settings;
            using (var fs = new System.IO.FileStream("config.json", System.IO.FileMode.Open))
            {
                settings = (Settings)serializer.ReadObject(fs);
            }

            var wasModified = false;
            var txtsettings = System.IO.File.ReadAllText("config.json");
            foreach (var p in settings.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (!txtsettings.Contains(p.Name))
                {
                    p.SetValue(settings, p.GetValue(defaultSettings));
                    wasModified = true;
                }
            }
            if (wasModified)
            {
                saveFile(serializer, settings);
            }

            return settings;
        }


        static void saveFile(DataContractJsonSerializer serializer, Settings s)
        {
            using (var fs = new System.IO.FileStream("config.json", System.IO.FileMode.Create))
            {
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(
                    fs, Encoding.UTF8, true, true, "  "))
                {
                    serializer.WriteObject(writer, s);
                    writer.Flush();
                }
            }
        }
    }



    [DataContract(Name = "Key", Namespace = "")]
    internal class EnumSurrogate
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        //[DataMember(Name = "Value")]
        //public int Value { get; set; }
    }

    internal class EnumeratorContractSurrogate : IDataContractSurrogate
    {
        public Type GetDataContractType(Type type)
        {
            return type == typeof(Enum) ? typeof(EnumSurrogate) : type;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            if (obj is EnumSurrogate)
            {
                return Enum.Parse(targetType, ((EnumSurrogate)obj).Name);
            }

            return obj;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj is Enum)
            {
                var pair = new EnumSurrogate
                {
                    Name = Enum.GetName(obj.GetType(), obj)
                    //, Value = (int)obj
                };
                return pair;
            }

            return obj;
        }

        object IDataContractSurrogate.GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        object IDataContractSurrogate.GetCustomDataToExport(System.Reflection.MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        void IDataContractSurrogate.GetKnownCustomDataTypes(System.Collections.ObjectModel.Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        Type IDataContractSurrogate.GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        System.CodeDom.CodeTypeDeclaration IDataContractSurrogate.ProcessImportedType(System.CodeDom.CodeTypeDeclaration typeDeclaration, System.CodeDom.CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
    }


}
