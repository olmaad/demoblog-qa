using DemoBlog.DataLib.Models;

namespace DemoBlog.DataLib.Arguments
{
    public class VoteCreateArguments
    {
        public string SessionKey { get; set; }
        public Vote Vote { get; set; }
    }
}
