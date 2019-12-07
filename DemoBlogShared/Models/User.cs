using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogShared.Models
{
    public class User : ICloneable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Login { get; set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; } = 1;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
