using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBlogBaseBuilder
{
    [JsonObject(MemberSerialization.OptIn)]
    class CommentData
    {
        [JsonProperty]
        public long PostId { get; set; }
        [JsonProperty]
        public long UserId { get; set; }
        [JsonProperty]
        public string Text { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    class CommentsData
    {
        [JsonProperty]
        public IList<CommentData> Comments { get; set; }
    }
}
