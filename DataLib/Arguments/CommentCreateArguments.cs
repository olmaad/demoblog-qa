using DemoBlog.DataLib.Models;

namespace DemoBlog.DataLib.Arguments
{
    public class CommentCreateArguments
    {
        public string SessionKey { get; set; }
        public long PostId { get; set; }
        public string Text { get; set; }
    }
}
