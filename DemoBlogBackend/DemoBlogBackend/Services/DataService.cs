using DemoBlogBackend.Models;

namespace DemoBlogBackend.Services
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
