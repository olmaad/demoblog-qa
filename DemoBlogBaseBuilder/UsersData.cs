using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBlogBaseBuilder
{
    [JsonObject(MemberSerialization.OptIn)]
    class UserData
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

    [JsonObject(MemberSerialization.OptIn)]
    class UsersData
    {
        [JsonProperty]
        public IList<UserData> Users { get; set; }
    }
}
