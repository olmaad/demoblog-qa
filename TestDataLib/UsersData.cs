using Newtonsoft.Json;

namespace DemoBlog.TestDataLib
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserData
    {
        [JsonProperty]
        public long Id { get; set; }
        [JsonProperty]
        public string Login { get; set; }
        [JsonProperty]
        public string Password { get; set; }
        [JsonProperty]
        public string Name { get; set; }
    }
}
