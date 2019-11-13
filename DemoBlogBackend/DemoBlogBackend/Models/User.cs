using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogBackend.Models
{
    public class User : ICloneable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
