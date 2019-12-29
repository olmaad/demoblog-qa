using Newtonsoft.Json;
using System.IO;

namespace DemoBlog.UiTestLib.Environment
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EnvironmentSettings
    {
        [JsonProperty]
        public string DriverType { get; set; }

        [JsonProperty]
        public string DriverPath { get; set; }

        [JsonProperty]
        public int DriverWaitTimeout { get; set; }

        [JsonProperty]
        public string BrowserExecutablePath { get; set; }

        [JsonProperty]
        public bool Headless { get; set; }

        [JsonProperty]
        public string BaseUrl { get; set; }

        public static EnvironmentSettings Load(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                var dataString = reader.ReadToEnd();

                return JsonConvert.DeserializeObject<EnvironmentSettings>(dataString);
            }
        }
    }
}
