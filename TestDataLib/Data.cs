using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDataLib
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
