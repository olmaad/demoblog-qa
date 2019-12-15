using DemoBlog.DataLib.Models;

namespace DemoBlog.DataLib.Arguments
{
    public class PostCreateArguments
    {
        public string SessionKey { get; set; }
        public Post Post { get; set; }
    }
}
