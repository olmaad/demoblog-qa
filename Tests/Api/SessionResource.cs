using DemoBlog.ApiTestLib;
using DemoBlog.DataLib.Bundles;
using DemoBlog.DataLib.Models;
using DemoBlog.TestDataLib;
using DemoBlog.Tests.Helpers;
using DemoBlog.Tests.Resources;
using DemoBlog.TestDataLib.Loader;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace DemoBlog.Tests.Api
{
    [TestFixture]
    public class SessionResource
    {
        DataLoaderFactory mDataLoaderFactory;
        Client mClient;
        SessionHelper mSessionHelper;

        public SessionResource()
        {
            mClient = new Client("http://localhost:8080");
            mSessionHelper = new SessionHelper(mClient);
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            mDataLoaderFactory = new DataLoaderFactory()
            {
                BaseDirectoryPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../TestData/ApiSession")
            };
        }

        [TestCase("CreateSession/data1.json")]
        [TestCase("CreateSession/data2.json")]
        public void CreateSession(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var user = loader.Data.Users.First();

            var bundle = mSessionHelper.Create(user);

            mSessionHelper.AssertSessionBundle(bundle, DataConverter.ToModelType(user, DataConverter.OutputTypeData));
        }

        [TestCase("RecreateSession/data1.json")]
        [TestCase("RecreateSession/data2.json")]
        public void RecreateSession(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var user = loader.Data.Users.First();

            var bundle = mSessionHelper.Create(user);

            mSessionHelper.AssertSessionBundle(bundle, DataConverter.ToModelType(user, DataConverter.OutputTypeData));

            bundle = mSessionHelper.Create(user);

            mSessionHelper.AssertSessionBundle(bundle, DataConverter.ToModelType(user, DataConverter.OutputTypeData));
        }

        [TestCase("CreateSessionNegative/data1.json")]
        [TestCase("CreateSessionNegative/data2.json")]
        [TestCase("CreateSessionNegative/data3.json")]
        [TestCase("CreateSessionNegative/data4.json")]
        public void CreateSessionNegative(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var bundle = mSessionHelper.Create(loader.Data.Users.First());

            AssertSessionBundleNegative(bundle);
        }

        [TestCase("RestoreSession/data1.json")]
        [TestCase("RestoreSession/data2.json")]
        public void RestoreSession(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var user = loader.Data.Users.First();

            var bundle = mSessionHelper.Create(user);

            mSessionHelper.AssertSessionBundle(bundle, DataConverter.ToModelType(user, DataConverter.OutputTypeData));

            var restoredBundle = mSessionHelper.Restore(bundle.Session.RestoreKey);

            mSessionHelper.AssertSessionBundle(restoredBundle, DataConverter.ToModelType(user, DataConverter.OutputTypeData));
        }

        [TestCase("")]
        [TestCase("87730e807ebbb9e1e626f5c38c3f52b5a28bdba78497d646248dec562649ce1001bdf3146e81793d620c44579258df74d714ce1322a42313da5d8b9ce5f60f78c67cb23bcad42d6d90b194c180c9f0a3fb01f49f357ed5d15f5484c8ba3bb2621d8f5f5f83091888de00bcec3adfec6e802985deea14bd24cb015b425a454723")]
        public void RestoreSessionNegative(string key)
        {
            var bundle = mSessionHelper.Restore(key);

            AssertSessionBundleNegative(bundle);
        }

        [TestCase("RemoveSession/data1.json")]
        [TestCase("RemoveSession/data2.json")]
        public void RemoveSession(string inputDataPath)
        {
            var loader = mDataLoaderFactory.Create(inputDataPath);

            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var user = loader.Data.Users.First();

            var bundle = mSessionHelper.Create(user);

            mSessionHelper.AssertSessionBundle(bundle, DataConverter.ToModelType(user, DataConverter.OutputTypeData));

            var ok = mClient.DeleteSessionAsync(bundle.Session.Key).Result;

            Assert.That(ok, Is.True, Strings.ErrorReturned);
        }

        private void AssertSessionBundleNegative(SessionBundle bundle)
        {
            Assert.Multiple(() =>
            {
                Assert.That(bundle.Session.Valid, Is.False, Strings.SuccessReturned);
                Assert.That(bundle.User, Is.Null, Strings.WrongUser);
            });
        }
    }
}
