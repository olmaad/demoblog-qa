using DemoBlog.ApiTestLib;
using DemoBlog.TestDataLib;
using DemoBlog.Tests.Helpers;
using DemoBlog.Tests.Resources;
using NUnit.Framework;
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
    }
}
