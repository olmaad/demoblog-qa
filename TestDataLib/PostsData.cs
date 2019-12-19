using Newtonsoft.Json;

namespace DemoBlog.TestDataLib
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PostData
    {
        [JsonProperty]
        public long Id { get; set; }
        [JsonProperty]
        public long UserId { get; set; }
        [JsonProperty]
        public string Title { get; set; }
        [JsonProperty]
        public string Preview { get; set; }
        [JsonProperty]
        public string Content { get; set; }
        [JsonProperty]
        public int DateOffset { get; set; }
    }
}
