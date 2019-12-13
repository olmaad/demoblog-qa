using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlog.DataLib.Models
{
    public class User : ICloneable, IEquatable<User>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Login { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; } = 1;

        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(User other)
        {
            if (other == null)
            {
                return false;
            }

            return (
                Id == other.Id &&
                Login == other.Login &&
                Name == other.Name &&
                Math.Abs(Rating - other.Rating) < 0.001);
        }

        public override bool Equals(object other)
        {
            return Equals(other as User);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
