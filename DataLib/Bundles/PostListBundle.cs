using DemoBlog.DataLib.Models;
using System.Collections.Generic;

namespace DemoBlog.DataLib.Bundles
{
    public class PostListBundle
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
    }
}
