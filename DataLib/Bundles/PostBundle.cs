using DemoBlog.DataLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBlog.DataLib.Bundles
{
    public class PostBundle
    {
        public Post Post { get; set; }
        public User User { get; set; }
        public Vote Vote { get; set; }
    }
}
