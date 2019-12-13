using Newtonsoft.Json;
using System.Collections.Generic;

namespace DemoBlog.TestDataLib
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Data
    {
        public string Name { get; set; }

        [JsonProperty]
        public IList<PostData> Posts { get; set; }

        [JsonProperty]
        public IList<UserData> Users { get; set; }

        [JsonProperty]
        public IList<CommentData> Comments { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
