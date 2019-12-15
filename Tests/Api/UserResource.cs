using DemoBlog.ApiTestLib;
using DemoBlog.TestDataLib;
using DemoBlog.Tests.Resources;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlog.Tests.Api
{
    [TestFixture]
    public class UserResource
    {
        DataLoaderFactory mDataLoaderFactory;
        Client mClient;

        public UserResource()
        {
            mClient = new Client("http://localhost:8080/");
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            mDataLoaderFactory = new DataLoaderFactory()
            {
                BaseDirectoryPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../TestData/ApiUser")
            };
        }

        [TestCase("CreateUser/data1.json")]
        public void CreateUser(string expectedDataPath)
        {
            var loader = mDataLoaderFactory.Create(expectedDataPath);

            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var user = DataConverter.ToModelType(loader.Data.Users.First(), DataConverter.OutputTypeCreate);

            var ok = Task.Run(async () => await mClient.PostUser(user)).Result;

            Assert.That(ok, Is.EqualTo(true), Strings.ErrorReturned);
        }

        [TestCase("CreateUserNegative/data1.json")]
        [TestCase("CreateUserNegative/data2.json")]
        [TestCase("CreateUserNegative/data3.json")]
        [TestCase("CreateUserNegative/data4.json")]
        [TestCase("CreateUserNegative/data5.json")]
        public void CreateUserNegative(string expectedDataPath)
        {
            var loader = mDataLoaderFactory.Create(expectedDataPath);

            Assume.That(loader.Data.Users, Has.Exactly(1).Items, Strings.WrongTestDataUserAmount);

            var user = DataConverter.ToModelType(loader.Data.Users.First(), DataConverter.OutputTypeCreate);

            var ok = Task.Run(async () => await mClient.PostUser(user)).Result;

            Assert.That(ok, Is.EqualTo(false), Strings.SuccessReturned);
        }
    }
}
