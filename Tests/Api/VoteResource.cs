using DemoBlog.ApiTestLib;
using DemoBlog.Tests.Helpers;
using DemoBlog.TestDataLib.Loader;
using NUnit.Framework;
using System.IO;
using DemoBlog.Tests.Resources;
using DemoBlog.TestDataLib;
using System.Linq;
using System.Collections.Generic;

namespace DemoBlog.Tests.Api
{
    [TestFixture("postCreate.json")]
    public class VoteResource
    {
        string mPostCreateDataPath;
        DataLoaderFactory mDataLoaderFactory;
        Client mClient;
        SessionHelper mSessionHelper;
        VoteHelper mVoteHelper;
        PostHelper mPostHelper;
        Dictionary<string, long> mPostIds;

        public VoteResource(string postCreateDataPath)
        {
            mPostCreateDataPath = postCreateDataPath;

            mClient = new Client("http://localhost:8080");
            mSessionHelper = new SessionHelper(mClient);
            mVoteHelper = new VoteHelper(mClient);
            mPostHelper = new PostHelper(mClient);
            mPostIds = new Dictionary<string, long>();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            mDataLoaderFactory = new DataLoaderFactory()
            {
                BaseDirectoryPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../TestData/ApiVote")
            };
        }

        /// <summary>
        /// Creating post for every test to avoid rating or already existing votes side effects
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            var loader = mDataLoaderFactory.Create(mPostCreateDataPath);

            Assume.That(loader.Data.Posts, Has.Exactly(1).Items, Strings.WrongTestDataPostAmount);
            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var session = mSessionHelper.CreateAndAssert(loader.Data.Users.Single());

            var post = loader.Data.Posts.Single();
            post.Preview = TestContext.CurrentContext.Test.Name;

            var postId = mPostHelper.CreateAndAssert(session.Session.Key, post);

            mPostIds.Add(TestContext.CurrentContext.Test.ID, postId);
        }

        [TestCase("CreateVote/data1.json")]
        public void CreateVote(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);
            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var session = mSessionHelper.CreateAndAssert(loader.Data.Users.Single());

            var vote = loader.Data.Votes.Single();
            vote.EntityId = mPostIds[TestContext.CurrentContext.Test.ID];

            var id = mClient.PostVoteAsync(DataConverter.ToModelType(session.Session.Key, vote, DataConverter.OutputTypeCreate)).Result;

            Assert.That(id, Is.GreaterThan(-1), Strings.ErrorReturned);
        }
        
        [TestCase("CreateVoteNegativeDublicate/data1.json")]
        public void CreateVoteNegativeDublicate(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);
            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var session = mSessionHelper.CreateAndAssert(loader.Data.Users.Single());

            var vote = loader.Data.Votes.Single();
            vote.EntityId = mPostIds[TestContext.CurrentContext.Test.ID];

            mVoteHelper.CreateAndAssert(session.Session.Key, vote);

            var id = mVoteHelper.Create(session.Session.Key, vote);

            Assert.That(id, Is.EqualTo(-1), Strings.SuccessReturned);
        }
        
        [TestCase("", "CreateVoteAnonymousNegative/data1.json")]
        public void CreateVoteAnonynousNegative(string sessionKey, string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);

            var vote = loader.Data.Votes.Single();
            vote.EntityId = mPostIds[TestContext.CurrentContext.Test.ID];

            var id = mClient.PostVoteAsync(DataConverter.ToModelType(sessionKey, vote, DataConverter.OutputTypeCreate)).Result;

            Assert.That(id, Is.EqualTo(-1), Strings.SuccessReturned);
        }

        [TestCase("EditVote/data1.json")]
        public void EditVote(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);
            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var session = mSessionHelper.CreateAndAssert(loader.Data.Users.Single());

            var vote = loader.Data.Votes.Single();
            vote.EntityId = mPostIds[TestContext.CurrentContext.Test.ID];

            var id = mVoteHelper.CreateAndAssert(session.Session.Key, vote);

            vote.Value = vote.Value * (-1);

            var ok = mClient.PutVoteAsync(id, DataConverter.ToModelType(session.Session.Key, vote, DataConverter.OutputTypeCreate)).Result;

            Assert.That(ok, Is.True, Strings.ErrorReturned);
        }

        [TestCase("", "EditVoteAnonymousNegative/data1.json")]
        public void EditVoteAnonymousNegative(string sessionKey, string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);

            var vote = loader.Data.Votes.Single();
            vote.EntityId = mPostIds[TestContext.CurrentContext.Test.ID];

            var ok = mClient.PutVoteAsync(0, DataConverter.ToModelType(sessionKey, vote, DataConverter.OutputTypeCreate)).Result;

            Assert.That(ok, Is.False, Strings.SuccessReturned);
        }

        [TestCase("RemoveVote/data1.json")]
        public void RemoveVote(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);
            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var session = mSessionHelper.CreateAndAssert(loader.Data.Users.Single());

            var vote = loader.Data.Votes.Single();
            vote.EntityId = mPostIds[TestContext.CurrentContext.Test.ID];

            var id = mVoteHelper.CreateAndAssert(session.Session.Key, vote);

            var ok = mClient.DeleteVoteAsync(id, session.Session.Key).Result;

            Assert.That(ok, Is.True, Strings.ErrorReturned);
        }

        [TestCase("RemoveVoteNegativeNotExists/data1.json")]
        public void RemoveVoteNegativeNotExists(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);
            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var session = mSessionHelper.CreateAndAssert(loader.Data.Users.Single());

            var vote = loader.Data.Votes.Single();
            vote.EntityId = mPostIds[TestContext.CurrentContext.Test.ID];

            var id = mVoteHelper.CreateAndAssert(session.Session.Key, vote);

            var ok = mClient.DeleteVoteAsync(id, session.Session.Key).Result;

            Assert.That(ok, Is.True, Strings.ErrorReturned);

            ok = mClient.DeleteVoteAsync(id, session.Session.Key).Result;

            Assert.That(ok, Is.False, Strings.SuccessReturned);
        }

        [TestCase("", "RemoveVoteAnonymousNegative/data1.json")]
        public void RemoveVoteAnonymousNegative(string sessionKey, string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Votes, Has.Exactly(1).Items, Strings.WrongTestDataVoteAmount);
            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var session = mSessionHelper.CreateAndAssert(loader.Data.Users.Single());

            var vote = loader.Data.Votes.Single();
            vote.EntityId = mPostIds[TestContext.CurrentContext.Test.ID];

            var id = mVoteHelper.CreateAndAssert(session.Session.Key, vote);

            var ok = mClient.DeleteVoteAsync(id, session.Session.Key).Result;

            Assert.That(ok, Is.True, Strings.ErrorReturned);

            ok = mClient.DeleteVoteAsync(id, sessionKey).Result;

            Assert.That(ok, Is.False, Strings.SuccessReturned);
        }
    }
}
