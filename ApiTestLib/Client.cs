using DemoBlog.DataLib.Arguments;
using DemoBlog.DataLib.Bundles;
using DemoBlog.DataLib.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["date"] = date.ToString("yyyy-MM-dd:zzz");

            var response = await mClient.GetStringAsync(mHost + "/api/posts?" + query.ToString());

            var bundle = JsonConvert.DeserializeObject<PostListBundle>(response, mSerializerSettings);

            return bundle;
        }

        public async Task<PostBundle> GetSpecificPost(long id)
        {
            var response = await mClient.GetStringAsync(mHost + "/api/posts/" + id);

            var bundle = JsonConvert.DeserializeObject<PostBundle>(response, mSerializerSettings);

            return bundle;
        }

        public async Task<long> PostPost(PostCreateArguments arguments)
        {
            var response = await mClient.PostAsync(mHost + "/api/posts", new StringContent(JsonConvert.SerializeObject(arguments), Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            long id = long.Parse(responseString);

            return id;
        }

        public async Task<bool> PostUser(UserCreateArguments user)
        {
            var response = await mClient.PostAsync(mHost + "/api/user", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<SessionBundle> PostSessionAsync(SessionCreateArguments arguments)
        {
            var response = await mClient.PostAsync(mHost + "/api/session", new StringContent(JsonConvert.SerializeObject(arguments), Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            var bundle = JsonConvert.DeserializeObject<SessionBundle>(responseString, mSerializerSettings);

            return bundle;
        }

        public async Task<bool> DeleteSessionAsync(string key)
        {
            var response = await mClient.DeleteAsync(mHost + "/api/session/" + key);

            return response.IsSuccessStatusCode;
        }

        public async Task<long> PostCommentAsync(CommentCreateArguments arguments)
        {
            var response = await mClient.PostAsync(mHost + "/api/comment", new StringContent(JsonConvert.SerializeObject(arguments), Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            long id = long.Parse(responseString);

            return id;
        }
        
        public async Task<CommentBundle> GetCommentsAsync(long postId, long? userId = null)
        {
            var responseString = await mClient.GetStringAsync(mHost + "/api/comment/" + postId);

            var bundle = JsonConvert.DeserializeObject<CommentBundle>(responseString, mSerializerSettings);

            return bundle;
        }

        public async Task<long> PostVoteAsync(VoteCreateArguments arguments)
        {
            var response = await mClient.PostAsync(mHost + "/api/vote", new StringContent(JsonConvert.SerializeObject(arguments), Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            long id = long.Parse(responseString);

            return id;
        }

        public async Task<bool> PutVoteAsync(long id, VoteCreateArguments arguments)
        {
            var response = await mClient.PutAsync(mHost + "/api/vote/" + id, new StringContent(JsonConvert.SerializeObject(arguments), Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteVoteAsync(long id, string sessionKey)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["sessionKey"] = sessionKey;

            var response = await mClient.DeleteAsync(mHost + "/api/vote/" + id + "?" + query.ToString());

            return response.IsSuccessStatusCode;
        }
    }
}
