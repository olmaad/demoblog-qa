using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBlogBaseBuilder
{
    [JsonObject(MemberSerialization.OptIn)]
    class PostData
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

    [JsonObject(MemberSerialization.OptIn)]
    class PostsData
    {
        [JsonProperty]
        public IList<PostData> Posts { get; set; }
    }
}
