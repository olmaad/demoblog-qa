using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogShared.Models
{
    public class Session : ICloneable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public long UserId { get; set; }
        public bool Valid { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
