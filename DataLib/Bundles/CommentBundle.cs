using DemoBlog.DataLib.Models;
using System.Collections.Generic;

namespace DemoBlog.DataLib.Bundles
{
    public class CommentBundle
    {
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
    }
}
