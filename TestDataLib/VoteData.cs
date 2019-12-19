using DemoBlog.DataLib.Models;
using Newtonsoft.Json;

namespace DemoBlog.TestDataLib
{
    [JsonObject(MemberSerialization.OptIn)]
    public class VoteData
    {
        public enum EntityType
        {
            Post = Vote.EntityType.Post,
            Comment = Vote.EntityType.Comment
        }

        [JsonProperty]
        public EntityType Type { get; set; }

        [JsonProperty]
        public long EntityId { get; set; }

        [JsonProperty]
        public long UserId { get; set; }

        [JsonProperty]
        public int Value { get; set; }
    }
}
