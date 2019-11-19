using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogBackend.Models
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long UserId { get; set; }
        public string Title { get; set; }
        public string Preview { get; set; }
        public string Content { get; set; }
    }
}
