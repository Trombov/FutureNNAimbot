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
        public string Game { get; set; } = "r5apex";
        [DataMember]
        public bool SimpleRCS { get; set; } = true;
        [DataMember]
        public int SimpleRCSvalue { get; set; } = 3;
        [DataMember]
        public Keys TrainModeKey { get; set; } = Keys.Insert;
        [DataMember]
        public Keys ScreenshotKey { get; set; } = Keys.Home;
        [DataMember]
        public Keys ScreenshotModeKey { get; set; } = Keys.NumPad9;
        [DataMember]
        public float SmoothAim { get; set; } = 0;
        [DataMember]
        public bool Head { get; set; } = false;
        [DataMember]
        public bool DrawAreaRectangle { get; set; } = false;
        [DataMember]
        public bool CursorToCenter { get; set; } = true;
        [DataMember]
        public bool DrawText { get; set; } = true;
        [DataMember]
        public byte DrawOpacity { get; set; } = 120;
        [DataMember]
        public int AutoAimDelayMs { get; set; } = 250;
        [DataMember]
        public IEnumerable<Keys> ShootKeys { get; set; } = new[] { Keys.LButton, Keys.RButton, Keys.Alt };


        static public Settings ReadSettings()
        {
            // Read settings
            var knowTypes = new List<Type> { typeof(EnumSurrogate) };
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Settings),
                knowTypes, int.MaxValue, false, new EnumeratorContractSurrogate(), false);

            Settings settings = null;
            using (var fs = new System.IO.FileStream("config.json", System.IO.FileMode.OpenOrCreate))
            {
                if (fs.Length == 0)
                {
                    settings = new Settings();
                    using (var writer = JsonReaderWriterFactory.CreateJsonWriter(
                        fs, Encoding.UTF8, true, true, "  "))
                    {
                        serializer.WriteObject(writer, settings);
                        writer.Flush();
                    }
                    //serializer.WriteObject(fs, settings);
                    MessageBox.Show($"Created auto-config, change whatever settings you want and restart.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                    return null;
                }
                else
                {
                    var t = (Settings)serializer.ReadObject(fs);
                    foreach (var p in t.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                    {
                        if (p.GetValue(t) == null)
                            p.SetValue(t, p.GetValue(settings));
                    }
                    settings = t;
                }
                return settings ?? new Settings();
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

    public class EnumeratorContractSurrogate : IDataContractSurrogate
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
