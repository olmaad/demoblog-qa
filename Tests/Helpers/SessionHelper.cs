using DemoBlog.ApiTestLib;
using DemoBlog.DataLib.Arguments;
using DemoBlog.DataLib.Bundles;
using DemoBlog.TestDataLib;

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

        public SessionBundle Restore(string restoreKey)
        {
            var arguments = new SessionCreateArguments()
            {
                Restore = true,
                RestoreKey = restoreKey
            };

            return mClient.PostSessionAsync(arguments).Result;
        }
    }
}
