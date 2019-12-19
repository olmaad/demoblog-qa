using DemoBlog.ApiTestLib;
using DemoBlog.DataLib.Bundles;
using DemoBlog.DataLib.Models;
using DemoBlog.TestDataLib;
using DemoBlog.TestDataLib.Loader;
using DemoBlog.Tests.Helpers;
using DemoBlog.Tests.Resources;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DemoBlog.Tests.Api
{
    [TestFixture]
    public class CommentResource
    {
        DataLoaderFactory mDataLoaderFactory;
        Client mClient;
        SessionHelper mSessionHelper;

        public CommentResource()
        {
            mClient = new Client("http://localhost:8080/");
            mSessionHelper = new SessionHelper(mClient);
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            mDataLoaderFactory = new DataLoaderFactory()
            {
                BaseDirectoryPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../TestData/ApiComment")
            };
        }

        [TestCase("CreateComment/data1.json")]
        [TestCase("CreateComment/data2.json")]
        public void CreateComment(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);
            Assume.That(loader.Data.Comments, Has.Exactly(1).Items, Strings.WrongTestDataCommentAmount);

            var sessionBundle = mSessionHelper.Create(loader.Data.Users.First());

            mSessionHelper.AssertSessionBundle(sessionBundle, DataConverter.ToModelType(loader.Data.Users.First(), DataConverter.OutputTypeData));

            var id = mClient.PostCommentAsync(DataConverter.ToModelType(sessionBundle.Session.Key, loader.Data.Comments.First(), DataConverter.OutputTypeCreate)).Result;

            Assert.That(id, Is.Not.EqualTo(-1), Strings.ErrorReturned);
        }

        [TestCase("CreateCommentNegativeInvalidData/data1.json")]
        [TestCase("CreateCommentNegativeInvalidData/data2.json")]
        public void CreateCommentNegativeInvalidData(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);
            Assume.That(loader.Data.Comments, Has.Exactly(1).Items, Strings.WrongTestDataCommentAmount);

            var sessionBundle = mSessionHelper.Create(loader.Data.Users.First());

            mSessionHelper.AssertSessionBundle(sessionBundle, DataConverter.ToModelType(loader.Data.Users.First(), DataConverter.OutputTypeData));

            var id = mClient.PostCommentAsync(DataConverter.ToModelType(sessionBundle.Session.Key, loader.Data.Comments.First(), DataConverter.OutputTypeCreate)).Result;

            Assert.That(id, Is.EqualTo(-1), Strings.IdReturnedInsteadOfError);
        }

        [TestCase("", "CreateCommentNegativeNoSession/data1.json")]
        [TestCase("87730e807ebbb9e1e626f5c38c3f52b5a28bdba78497d646248dec562649ce1001bdf3146e81793d620c44579258df74d714ce1322a42313da5d8b9ce5f60f78c67cb23bcad42d6d90b194c180c9f0a3fb01f49f357ed5d15f5484c8ba3bb2621d8f5f5f83091888de00bcec3adfec6e802985deea14bd24cb015b425a454723", "CreateCommentNegativeNoSession/data2.json")]
        public void CreateCommentNegativeNoSession(string sessionKey, string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Comments, Has.Exactly(1).Items, Strings.WrongTestDataCommentAmount);

            var id = mClient.PostCommentAsync(DataConverter.ToModelType(sessionKey, loader.Data.Comments.First(), DataConverter.OutputTypeCreate)).Result;

            Assert.That(id, Is.EqualTo(-1), Strings.IdReturnedInsteadOfError);
        }

        [TestCase(2, "LoadCommentListAnonymous/data1.json")]
        public void LoadCommentListAnonymous(long postId, string expectedDataPath)
        {
            var loader = mDataLoaderFactory.Create(expectedDataPath);

            var comments = loader.Data.Comments.Select(c => DataConverter.ToModelType(c, DataConverter.OutputTypeData));
            var users = loader.Data.Users.Select(u => DataConverter.ToModelType(u, DataConverter.OutputTypeData));

            var bundle = mClient.GetCommentsAsync(postId).Result;

            AssertCommentBundle(bundle, comments, users);
        }

        private void AssertCommentBundle(CommentBundle bundle, IEnumerable<Comment> comments, IEnumerable<User> users)
        {
            Assert.Multiple(() =>
            {
                Assert.That(bundle.Comments, Is.EquivalentTo(comments), Strings.WrongCommentList);
                Assert.That(bundle.Users, Is.EquivalentTo(users), Strings.WrongUserList);
            });
        }
    }
}
