using DemoBlog.ApiTestLib;
using DemoBlog.Tests.Helpers;
using DemoBlog.TestDataLib.Loader;
using NUnit.Framework;
using System.IO;
using DemoBlog.Tests.Resources;
using DemoBlog.TestDataLib;
using System.Linq;

namespace DemoBlog.Tests.Api
{
    [TestFixture]
    public class VoteResource
    {
        DataLoaderFactory mDataLoaderFactory;
        Client mClient;
        SessionHelper mSessionHelper;

        public VoteResource()
        {
            mClient = new Client("http://localhost:8080/");
            mSessionHelper = new SessionHelper(mClient);
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            mDataLoaderFactory = new DataLoaderFactory()
            {
                BaseDirectoryPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../TestData/ApiVote")
            };
        }

        [Order(1)]
        [TestCase("CreateVote/data1.json")]
        public void CreateVote(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);
            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var session = mSessionHelper.CreateAndAssert(loader.Data.Users.First());

            var ok = mClient.PostVoteAsync(DataConverter.ToModelType(session.Session.Key, loader.Data.Votes.First(), DataConverter.OutputTypeCreate)).Result;

            Assert.That(ok, Is.True, Strings.ErrorReturned);
        }

        [Order(2)]
        [TestCase("CreateVoteNegativeDublicate/data1.json")]
        public void CreateVoteNegativeDublicate(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);
            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var session = mSessionHelper.CreateAndAssert(loader.Data.Users.First());

            var ok = mClient.PostVoteAsync(DataConverter.ToModelType(session.Session.Key, loader.Data.Votes.First(), DataConverter.OutputTypeCreate)).Result;

            Assert.That(ok, Is.True, Strings.ErrorReturned);

            ok = mClient.PostVoteAsync(DataConverter.ToModelType(session.Session.Key, loader.Data.Votes.First(), DataConverter.OutputTypeCreate)).Result;

            Assert.That(ok, Is.False, Strings.SuccessReturned);
        }
        
        [TestCase("", "CreateVoteAnonynousNegative/data1.json")]
        public void CreateVoteAnonynousNegative(string sessionKey, string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);

            var ok = mClient.PostVoteAsync(DataConverter.ToModelType(sessionKey, loader.Data.Votes.First(), DataConverter.OutputTypeCreate)).Result;

            Assert.That(ok, Is.False, Strings.SuccessReturned);
        }

        [Order(3)]
        [TestCase("EditVote/data1.json")]
        public void EditVote(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);
            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var session = mSessionHelper.CreateAndAssert(loader.Data.Users.First());

            var ok = mClient.PutVoteAsync(DataConverter.ToModelType(session.Session.Key, loader.Data.Votes.First(), DataConverter.OutputTypeCreate));

            Assert.That(ok, Is.True, Strings.ErrorReturned);
        }

        [Order(3)]
        [TestCase("", "EditVoteAnonynousNegative/data1.json")]
        public void EditVoteAnonynousNegative(string sessionKey, string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);

            var ok = mClient.PutVoteAsync(DataConverter.ToModelType(sessionKey, loader.Data.Votes.First(), DataConverter.OutputTypeCreate));

            Assert.That(ok, Is.False, Strings.SuccessReturned);
        }

        [Order(4)]
        [TestCase("RemoveVote/data1.json")]
        public void RemoveVote(string inputDataPath)
        {

        }
    }
}
