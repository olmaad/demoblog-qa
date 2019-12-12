using DemoBlogShared.Models;
using System.Collections.Generic;

namespace DemoBlogShared.Bundles
{
    public class PostsBundle
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
    }
}
