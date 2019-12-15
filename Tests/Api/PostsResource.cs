using DemoBlog.ApiTestLib;
using DemoBlog.DataLib.Bundles;
using DemoBlog.DataLib.Models;
using DemoBlog.TestDataLib;
using DemoBlog.Tests.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlog.Tests.Api
{
    [TestFixture]
    public class PostsResource
    {
        DataLoaderFactory mDataLoaderFactory;
        Client mClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            mDataLoaderFactory = new DataLoaderFactory()
            {
                BaseDirectoryPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../TestData/ApiPosts")
            };

            mClient = new Client("http://localhost:8080/");
        }

        [Order(1)]
        [TestCase("LoadPostListAnonymous/data.json")]
        public void LoadPostListAnonymous(string expectedDataPath)
        {
            var loader = mDataLoaderFactory.Create(expectedDataPath);

            var posts = loader.Data.Posts.Select(d => DataConverter.ToModel(d)).ToList();
            var users = loader.Data.Users.Select(d => DataConverter.ToModel(d)).ToList();

            var bundle = Task.Run(async () => await mClient.GetPostsAsync()).Result;

            AssertPostListBundle(bundle, posts, users);
        }

        [Order(1)]
        [TestCase(0, "LoadPostListAnonymousWithDate/data.json")]
        [TestCase(-1, "LoadPostListAnonymousWithDate/data2.json")]
        public void LoadPostListAnonymousWithDate(int dateOffset, string expectedDataPath)
        {
            var loader = mDataLoaderFactory.Create(expectedDataPath);

            var posts = loader.Data.Posts.Select(d => DataConverter.ToModel(d)).ToList();
            var users = loader.Data.Users.Select(d => DataConverter.ToModel(d)).ToList();

            var bundle = Task.Run(async () => await mClient.GetPostsByDateAsync(DateTime.UtcNow.Date + new TimeSpan(dateOffset, 0, 0, 0))).Result;

            AssertPostListBundle(bundle, posts, users);
        }

        [TestCase(1, "LoadSpecificPostAnonymous/data1.json")]
        [TestCase(2, "LoadSpecificPostAnonymous/data2.json")]
        public void LoadSpecificPostAnonymous(long id, string expectedDataPath)
        {
            var loader = mDataLoaderFactory.Create(expectedDataPath);

            Assume.That(loader.Data.Posts, Has.Exactly(1).Items, Strings.WrongTestDataPostAmount);
            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var post = DataConverter.ToModel(loader.Data.Posts.First());
            var user = DataConverter.ToModel(loader.Data.Users.First());

            var bundle = Task.Run(async () => await mClient.GetSpecificPost(id)).Result;

            AssertPostBundle(bundle, post, user);
        }

        [TestCase("CreatePostAnonymousNegative/data1.json")]
        public void CreatePostAnonymousNegative(string expectedDataPath)
        {
            var loader = mDataLoaderFactory.Create(expectedDataPath);

            Assume.That(loader.Data.Posts, Has.Exactly(1).Items, Strings.WrongTestDataPostAmount);

            var post = DataConverter.ToModel(loader.Data.Posts.First());

            var id = Task.Run(async () => await mClient.PostPost(post)).Result;

            Assert.That(id, Is.EqualTo(-1), Strings.IdReturnedInsteadOfError);
        }

        [TestCase("CreatePost/data1.json")]
        [TestCase("CreatePost/data2.json")]
        [TestCase("CreatePost/data3.json")]
        public void CreatePost(string expectedDataPath)
        {
            var loader = mDataLoaderFactory.Create(expectedDataPath);

            Assume.That(loader.Data.Posts, Has.Exactly(1).Items, Strings.WrongTestDataPostAmount);

            var post = DataConverter.ToModel(loader.Data.Posts.First());

            var id = Task.Run(async () => await mClient.PostPost(post)).Result;

            Assert.That(id, Is.Not.EqualTo(-1), Strings.ErrorReturned);
        }

        [TestCase("CreatePostNegative/data1.json")]
        [TestCase("CreatePostNegative/data2.json")]
        public void CreatePostNegative(string expectedDataPath)
        {
            var loader = mDataLoaderFactory.Create(expectedDataPath);

            Assume.That(loader.Data.Posts, Has.Exactly(1).Items, Strings.WrongTestDataPostAmount);

            var post = DataConverter.ToModel(loader.Data.Posts.First());

            var id = Task.Run(async () => await mClient.PostPost(post)).Result;

            Assert.That(id, Is.EqualTo(-1), Strings.IdReturnedInsteadOfError);
        }

        private void AssertPostListBundle(PostListBundle bundle, IEnumerable<Post> posts, IEnumerable<User> users)
        {
            Assert.Multiple(() =>
            {
                Assert.That(bundle.Posts, Is.EquivalentTo(posts), Strings.WrongPostList);
                Assert.That(bundle.Users, Is.EquivalentTo(users), Strings.WrongUserList);
            });
        }

        private void AssertPostBundle(PostBundle bundle, Post post, User user)
        {
            Assert.Multiple(() =>
            {
                Assert.That(bundle.Post, Is.EqualTo(post), Strings.WrongPost);
                Assert.That(bundle.User, Is.EqualTo(user), Strings.WrongUser);
            });
        }
    }
}
