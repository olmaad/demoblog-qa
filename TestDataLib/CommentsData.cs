using Newtonsoft.Json;

namespace DemoBlog.TestDataLib
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CommentData
    {
        [JsonProperty]
        public long PostId { get; set; }
        [JsonProperty]
        public long UserId { get; set; }
        [JsonProperty]
        public string Text { get; set; }
    }
}
