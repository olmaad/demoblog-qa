using DemoBlog.ApiTestLib;
using DemoBlog.TestDataLib;
using DemoBlog.Tests.Resources;
using DemoBlog.TestDataLib.Loader;
using NUnit.Framework;
using System.IO;
using System.Linq;
using DemoBlog.DataLib.Arguments;
using DemoBlog.TestDataLib.Tools;

namespace DemoBlog.Tests.Api
{
    [TestFixture]
    public class UserResource : ResourceBase
    {
        public UserResource()
        { }

        [OneTimeSetUp]
        public void SetUp()
        {
            BaseSetUp("ApiUser");
        }

        [TestCase]
        public void CreateUser()
        {
            var login = DataGenerator.GenerateUsername();
            var name = DataGenerator.GenerateUsername();
            var password = DataGenerator.GenerateUsername();

            var user = new UserCreateArguments()
            {
                Login = login,
                Name = name,
                Password = password
            };

            var ok = mClient.PostUser(user).Result;

            Assert.That(ok, Is.True, Strings.ErrorReturned);
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

            var ok = mClient.PostUser(user).Result;

            Assert.That(ok, Is.False, Strings.SuccessReturned);
        }
    }
}
