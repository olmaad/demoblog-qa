using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DemoBlog.UiTestLib.Environment
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EnvironmentSettings
    {
        [JsonProperty]
        public IList<EnvironmentSettingsItem> Settings { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class EnvironmentSettingsItem
    {
        [JsonProperty]
        public string DriverType { get; set; }

        [JsonProperty]
        public string DriverPath { get; set; }

        [JsonProperty]
        public string RemoteDriverHost { get; set; }

        [JsonProperty]
        public int RemoteDriverPort { get; set; }

        [JsonProperty]
        public int DriverWaitTimeout { get; set; }

        [JsonProperty]
        public string BrowserExecutablePath { get; set; }

        [JsonProperty]
        public bool Headless { get; set; }

        [JsonProperty]
        public string BaseUrl { get; set; }

        public static EnvironmentSettingsItem Load(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                var dataString = reader.ReadToEnd();

                return JsonConvert.DeserializeObject<EnvironmentSettingsItem>(dataString);
            }
        }
    }
}
