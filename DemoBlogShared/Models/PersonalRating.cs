using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogShared.Models
{
    public class PersonalRating : ICloneable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long UserId { get; set; }
        public long AuthorId { get; set; }
        public double Rating { get; set; } = 1;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
