using DemoBlog.DataLib.Models;

namespace DemoBlog.Backend.Services
{
    public class DataService
    {
        public BlogContext DbContext { get; private set; }

        public DataService(BlogContext context)
        {
            DbContext = context;
        }
    }
}
