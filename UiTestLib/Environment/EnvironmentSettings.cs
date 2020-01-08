using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DemoBlog.UiTestLib.Environment
{
    public class EnvironmentSettings
    {
        public IList<EnvironmentSettingsItem> Settings { get; set; }
    }

    public class EnvironmentSettingsItem
    {
        public string DriverType { get; set; }

        public string DriverPath { get; set; }

        public string RemoteDriverHost { get; set; }

        public int RemoteDriverPort { get; set; }

        public int DriverWaitTimeout { get; set; }

        public string BrowserExecutablePath { get; set; }

        public bool Headless { get; set; }

        public string BaseUrl { get; set; }

        public static EnvironmentSettingsItem Load(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                var dataString = reader.ReadToEnd();

                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                
                return deserializer.Deserialize<EnvironmentSettingsItem>(dataString);
            }
        }
    }
}
