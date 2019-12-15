using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlog.DataLib.Models
{
    public class Session : ICloneable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long UserId { get; set; }
        public bool Valid { get; set; }
        public string Key { get; set; }
        public string RestoreKey { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
