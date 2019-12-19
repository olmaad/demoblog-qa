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

        public long Create(string sessionKey, VoteData data)
        {
            var id = mClient.PostVoteAsync(DataConverter.ToModelType(sessionKey, data, DataConverter.OutputTypeCreate)).Result;

            return id;
        }

        public long CreateAndAssert(string sessionKey, VoteData data)
        {
            var id = mClient.PostVoteAsync(DataConverter.ToModelType(sessionKey, data, DataConverter.OutputTypeCreate)).Result;

            Assert.That(id, Is.GreaterThan(-1), Strings.ErrorReturned);

            return id;
        }
    }
}
