using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogBackend.Models
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
    }
}
