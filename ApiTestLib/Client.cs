using DemoBlog.DataLib.Bundles;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoBlog.ApiTestLib
{
    public class Client
    {
        protected string mHost;
        protected HttpClient mClient;

        public Client(string host)
        {
            mHost = host;

            mClient = new HttpClient();
            //mClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }

        public async Task<PostsBundle> GetPostsAsync()
        {
            var response = await mClient.GetStringAsync(mHost + "/api/posts");

            var bundle = JsonConvert.DeserializeObject<PostsBundle>(response, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });

            return bundle;
        }

        public async Task<PostsBundle> GetPostsByDateAsync(DateTime date)
        {
            var response = await mClient.GetStringAsync(mHost + "/api/posts?date=" + date.ToString("yyyy-MM-dd"));

            var bundle = JsonConvert.DeserializeObject<PostsBundle>(response, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });

            return bundle;
        }
    }
}
