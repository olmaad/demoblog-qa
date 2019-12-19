using DemoBlog.ApiTestLib;
using DemoBlog.TestDataLib;
using DemoBlog.Tests.Resources;
using NUnit.Framework;

namespace DemoBlog.Tests.Helpers
{
    public class PostHelper
    {
        private Client mClient;

        public PostHelper(Client client)
        {
            mClient = client;
        }

        public long Create(string sessionKey, PostData data)
        {
            return mClient.PostPost(DataConverter.ToModelType(sessionKey, data, DataConverter.OutputTypeCreate)).Result;
        }

        public long CreateAndAssert(string sessionKey, PostData data)
        {
            var id = Create(sessionKey, data);

            Assert.That(id, Is.GreaterThan(-1), Strings.ErrorReturned);

            return id;
        }
    }
}
