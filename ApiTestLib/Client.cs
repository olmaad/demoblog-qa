﻿using DemoBlog.DataLib.Bundles;
using DemoBlog.DataLib.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoBlog.ApiTestLib
{
    public class Client
    {
        protected string mHost;
        protected HttpClient mClient;
        protected JsonSerializerSettings mSerializerSettings;

        public Client(string host)
        {
            mHost = host;

            mClient = new HttpClient();

            mSerializerSettings = new JsonSerializerSettings()
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
        }

        public async Task<PostListBundle> GetPostsAsync()
        {
            var response = await mClient.GetStringAsync(mHost + "/api/posts");

            var bundle = JsonConvert.DeserializeObject<PostListBundle>(response, mSerializerSettings);

            return bundle;
        }

        public async Task<PostListBundle> GetPostsByDateAsync(DateTime date)
        {
            var response = await mClient.GetStringAsync(mHost + "/api/posts?date=" + date.ToString("yyyy-MM-dd"));

            var bundle = JsonConvert.DeserializeObject<PostListBundle>(response, mSerializerSettings);

            return bundle;
        }

        public async Task<PostBundle> GetSpecificPost(long id)
        {
            var response = await mClient.GetStringAsync(mHost + "/api/posts/" + id);

            var bundle = JsonConvert.DeserializeObject<PostBundle>(response, mSerializerSettings);

            return bundle;
        }

        public async Task<long> PostPost(Post post)
        {
            var response = await mClient.PostAsync(mHost + "/api/posts", new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            long id = long.Parse(responseString);

            return id;
        }
    }
}