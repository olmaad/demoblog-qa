using DemoBlog.ApiTestLib;
using DemoBlog.DataLib.Arguments;
using DemoBlog.DataLib.Bundles;
using DemoBlog.TestDataLib;
using System.Threading.Tasks;

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

            return Task.Run(async () => await mClient.PostSessionAsync(arguments)).Result;
        }
    }
}
