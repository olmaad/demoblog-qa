using DemoBlog.ApiTestLib;
using DemoBlog.DataLib.Arguments;
using DemoBlog.DataLib.Bundles;
using DemoBlog.DataLib.Models;
using DemoBlog.TestDataLib;
using DemoBlog.Tests.Resources;
using NUnit.Framework;

namespace DemoBlog.Tests.Helpers
{
    internal class SessionHelper
    {
        private Client mClient;

        public SessionHelper(Client client)
        {
            mClient = client;
        }

        public SessionBundle Create(UserData data)
        {
            var arguments = new SessionCreateArguments()
            {
                Restore = false,
                Login = data.Login,
                Password = data.Password
            };

            return mClient.PostSessionAsync(arguments).Result;
        }

        public SessionBundle CreateAndAssert(UserData data)
        {
            var bundle = Create(data);

            AssertSessionBundle(bundle, DataConverter.ToModelType(data, DataConverter.OutputTypeData));

            return bundle;
        }

        public SessionBundle Restore(string restoreKey)
        {
            var arguments = new SessionCreateArguments()
            {
                Restore = true,
                RestoreKey = restoreKey
            };

            return mClient.PostSessionAsync(arguments).Result;
        }

        public void AssertSessionBundle(SessionBundle bundle, User user)
        {
            Assert.Multiple(() =>
            {
                Assert.That(bundle.Session.Valid, Is.True, Strings.ErrorReturned);
                Assert.That(bundle.Session.UserId, Is.EqualTo(user.Id), Strings.WrongUser);
                Assert.That(bundle.User, Is.EqualTo(user), Strings.WrongUser);
            });
        }
    }
}
