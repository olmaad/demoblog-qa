using DemoBlog.ApiTestLib;
using DemoBlog.TestDataLib;
using DemoBlog.Tests.Resources;
using NUnit.Framework;

namespace DemoBlog.Tests.Helpers
{
    public class VoteHelper
    {
        private Client mClient;

        public VoteHelper(Client client)
        {
            mClient = client;
        }

        public bool Create(string sessionKey, VoteData data)
        {
            var ok = mClient.PostVoteAsync(DataConverter.ToModelType(sessionKey, data, DataConverter.OutputTypeCreate)).Result;

            return ok;
        }

        public void CreateAndAssert(string sessionKey, VoteData data)
        {
            var ok = mClient.PostVoteAsync(DataConverter.ToModelType(sessionKey, data, DataConverter.OutputTypeCreate)).Result;

            Assert.That(ok, Is.True, Strings.ErrorReturned);
        }
    }
}
