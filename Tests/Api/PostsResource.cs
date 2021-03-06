﻿using DemoBlog.ApiTestLib;
using DemoBlog.DataLib.Bundles;
using DemoBlog.DataLib.Models;
using DemoBlog.TestDataLib;
using DemoBlog.Tests.Helpers;
using DemoBlog.Tests.Resources;
using DemoBlog.TestDataLib.Loader;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlog.Tests.Api
{
    [TestFixture]
    public class PostsResource : ResourceBase
    {
        SessionHelper mSessionHelper;

        public PostsResource()
        { }

        [OneTimeSetUp]
        public void SetUp()
        {
            BaseSetUp("ApiPosts");

            mSessionHelper = new SessionHelper(mClient);
        }

        [TestCase("LoadPostListAnonymous/data.json")]
        public void LoadPostListAnonymous(string expectedDataPath)
        {
            var loader = mDataLoaderFactory.Create(expectedDataPath);

            var posts = loader.Data.Posts.Select(d => DataConverter.ToModelType(d, DataConverter.OutputTypeData)).ToList();
            var users = loader.Data.Users.Select(d => DataConverter.ToModelType(d, DataConverter.OutputTypeData)).ToList();

            var bundle = mClient.GetPostsAsync().Result;

            AssertPostListBundle(false, bundle, posts, users);
        }

        [TestCase(false, 0, "LoadPostListAnonymousWithDate/data.json")]
        [TestCase(true, -1, "LoadPostListAnonymousWithDate/data2.json")]
        public void LoadPostListAnonymousWithDate(bool strictAssert, int dateOffset, string expectedDataPath)
        {
            var loader = mDataLoaderFactory.Create(expectedDataPath);

            var posts = loader.Data.Posts.Select(d => DataConverter.ToModelType(d, DataConverter.OutputTypeData)).ToList();
            var users = loader.Data.Users.Select(d => DataConverter.ToModelType(d, DataConverter.OutputTypeData)).ToList();

            var bundle = mClient.GetPostsByDateAsync(DateTime.UtcNow.Date + new TimeSpan(dateOffset, 0, 0, 0)).Result;

            AssertPostListBundle(strictAssert, bundle, posts, users);
        }

        [TestCase(1, "LoadSpecificPostAnonymous/data1.json")]
        [TestCase(3, "LoadSpecificPostAnonymous/data2.json")]
        public void LoadSpecificPostAnonymous(long id, string expectedDataPath)
        {
            var loader = mDataLoaderFactory.Create(expectedDataPath);

            Assume.That(loader.Data.Posts, Has.Exactly(1).Items, Strings.WrongTestDataPostAmount);
            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var post = DataConverter.ToModelType(loader.Data.Posts.First(), DataConverter.OutputTypeData);
            var user = DataConverter.ToModelType(loader.Data.Users.First(), DataConverter.OutputTypeData);

            var bundle = mClient.GetSpecificPost(id).Result;

            AssertPostBundle(bundle, post, user);
        }

        [TestCase("CreatePostAnonymousNegative/data1.json")]
        public void CreatePostAnonymousNegative(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Posts, Has.Exactly(1).Items, Strings.WrongTestDataPostAmount);

            var post = DataConverter.ToModelType("", loader.Data.Posts.First(), DataConverter.OutputTypeCreate);

            var id = mClient.PostPost(post).Result;

            Assert.That(id, Is.EqualTo(-1), Strings.IdReturnedInsteadOfError);
        }

        [TestCase("CreatePost/data1.json")]
        [TestCase("CreatePost/data2.json")]
        [TestCase("CreatePost/data3.json")]
        public void CreatePost(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);
            Assume.That(loader.Data.Posts, Has.Exactly(1).Items, Strings.WrongTestDataPostAmount);

            var session = mSessionHelper.Create(loader.Data.Users.First());

            Assert.That(session.Session.Valid, Is.EqualTo(true), Strings.ErrorCreateSession);

            var post = DataConverter.ToModelType(session.Session.Key, loader.Data.Posts.First(), DataConverter.OutputTypeCreate);

            var id = mClient.PostPost(post).Result;

            Assert.That(id, Is.Not.EqualTo(-1), Strings.ErrorReturned);
        }

        [TestCase("CreatePostNegative/data1.json")]
        [TestCase("CreatePostNegative/data2.json")]
        public void CreatePostNegative(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Posts, Has.Exactly(1).Items, Strings.WrongTestDataPostAmount);

            var session = mSessionHelper.Create(loader.Data.Users.First());

            var post = DataConverter.ToModelType(session.Session.Key, loader.Data.Posts.First(), DataConverter.OutputTypeCreate);

            var id = mClient.PostPost(post).Result;

            Assert.That(id, Is.EqualTo(-1), Strings.IdReturnedInsteadOfError);
        }

        private void AssertPostListBundle(bool strict, PostListBundle bundle, IEnumerable<Post> posts, IEnumerable<User> users)
        {
            if (strict)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(bundle.Posts, Is.EquivalentTo(posts), Strings.WrongPostList);
                    Assert.That(bundle.Users, Is.EquivalentTo(users), Strings.WrongUserList);
                });
            }
            else
            {
                Assert.Multiple(() =>
                {
                    Assert.That(bundle.Posts, Is.SupersetOf(posts), Strings.WrongPostList);
                    Assert.That(bundle.Users, Is.SupersetOf(users), Strings.WrongUserList);
                });
            }
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
