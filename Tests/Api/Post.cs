using DemoBlog.ApiTestLib;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DemoBlog.Tests.Api
{
    [TestFixture]
    public class Post
    {
        Client mClient;

        [SetUp]
        public void SetUp()
        {
            mClient = new Client("http://localhost:8080/");
        }

        [TestCase("D:\\Dev\\Projects\\DemoBlog\\TestData\\LoadPostListAnonymous\\data.json")]
        public void LoadPostListAnonymous(string expectedDataPath)
        {
            var loader = new DataLoader(expectedDataPath);

            var bundle = Task.Run(async () => await mClient.GetPostsAsync()).Result;

            DemoBlog.ApiTestLib.Assert.PostBundle(bundle, loader.Data.Posts, loader.Data.Users);
        }

        [TestCase(0, "D:\\Dev\\Projects\\DemoBlog\\TestData\\LoadPostListAnonymousWithDate\\data.json")]
        [TestCase(-1, "D:\\Dev\\Projects\\DemoBlog\\TestData\\LoadPostListAnonymousWithDate\\data2.json")]
        public void LoadPostListAnonymousWithDate(int dateOffset, string expectedDataPath)
        {
            var loader = new DataLoader(expectedDataPath);

            var bundle = Task.Run(async () => await mClient.GetPostsByDateAsync(DateTime.UtcNow.Date + new TimeSpan(dateOffset, 0, 0, 0))).Result;

            DemoBlog.ApiTestLib.Assert.PostBundle(bundle, loader.Data.Posts, loader.Data.Users);
        }
    }
}
