using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogBackend.Models
{
    public class Vote : ICloneable
    {
        public enum EntityType
        {
            Post = 1,
            Comment = 2
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public EntityType Type { get; set; }
        public long EntityId { get; set; }
        public long UserId { get; set; }
        public int Value { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
