using DemoBlog.ApiTestLib;
using DemoBlog.DataLib.Bundles;
using DemoBlog.DataLib.Models;
using DemoBlog.TestDataLib;
using DemoBlog.Tests.Helpers;
using DemoBlog.Tests.Resources;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            mClient = new Client("http://localhost:8080/");
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

            AssertSessionBundle(bundle, DataConverter.ToModelType(user, DataConverter.OutputTypeData));
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

            Assert.Multiple(() =>
            {
                Assert.That(bundle.Session.Valid, Is.EqualTo(false), Strings.SuccessReturned);
                Assert.That(bundle.User, Is.EqualTo(null), Strings.WrongUser);
            });
        }

        private void AssertSessionBundle(SessionBundle bundle, User user)
        {
            Assert.Multiple(() =>
            {
                Assert.That(bundle.Session.Valid, Is.EqualTo(true), Strings.ErrorReturned);
                Assert.That(bundle.Session.UserId, Is.EqualTo(user.Id), Strings.WrongUser);
                Assert.That(bundle.User, Is.EqualTo(user), Strings.WrongUser);
            });
        }
    }
}
