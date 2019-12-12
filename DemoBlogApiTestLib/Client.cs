using DemoBlogShared.Bundles;
using DemoBlogShared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DemoBlogApiTestLib
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
