using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogBackend.Models
{
    public class Session
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public long UserId { get; set; }
        public bool Valid { get; set; }
    }
}
