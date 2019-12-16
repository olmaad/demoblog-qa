using DemoBlog.DataLib.Models;

namespace DemoBlog.DataLib.Arguments
{
    public class CommentCreateArguments
    {
        public string SessionKey { get; set; }
        public Comment Comment { get; set; }
    }
}
