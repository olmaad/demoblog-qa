using DemoBlog.ApiTestLib;
using DemoBlog.TestDataLib.Loader;
using DemoBlog.Tests.Resources;
using NUnit.Framework;
using System.IO;

namespace DemoBlog.Tests.Api
{
    public abstract class ResourceBase
    {
        protected Client mClient;
        protected DataLoaderFactory mDataLoaderFactory;
        
        protected void BaseSetUp(string dataSubdirectory)
        {
            var parameters = TestContext.Parameters;

            Assume.That(parameters.Names, Contains.Item("host"), string.Format(Strings.NoTestParameterPresent, "host"));
            Assume.That(parameters.Names, Contains.Item("testDataDirectory"), string.Format(Strings.NoTestParameterPresent, "testDataDirectory"));

            mClient = new Client(parameters["host"]);
            mDataLoaderFactory = new DataLoaderFactory()
            {
                BaseDirectoryPath = Path.Combine(parameters["testDataDirectory"], dataSubdirectory)
            };
        }
    }
}
