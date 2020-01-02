using DemoBlog.UiTestLib.Environment;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DemoBlog.Tests.Ui
{
    public class UiEnvironmentTestFixtureData
    {
        public static IEnumerable<TestFixtureData> FixtureParms
        {
            get
            {
                var dataDirectory = TestContext.Parameters["testDataDirectory"];
                var uiSettingsListFile = TestContext.Parameters["uiSettingsListFile"];

                using (var reader = new StreamReader(Path.Combine(dataDirectory, uiSettingsListFile)))
                {
                    var content = reader.ReadToEnd();
                    var settingsPathList = JsonConvert.DeserializeObject<IList<string>>(content);
                    
                    return settingsPathList.Select(p => new TestFixtureData(Path.Combine(dataDirectory, p))).ToList();
                }
            }
        }
    }

    [TestFixtureSource(typeof(UiEnvironmentTestFixtureData), "FixtureParms")]
    public abstract class UiEnvironmentTestFixture
    {
        protected string mEnvironmentSettingsPath;
        protected EnvironmentFactory mEnvironmentFactory;
        protected Dictionary<string, TestEnvironment> mEnvironmentByTestId;

        protected UiEnvironmentTestFixture(string environmentSettingsPath)
        {
            mEnvironmentSettingsPath = environmentSettingsPath;
            mEnvironmentByTestId = new Dictionary<string, TestEnvironment>();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            mEnvironmentFactory = new EnvironmentFactory(Path.Combine(TestContext.CurrentContext.TestDirectory, mEnvironmentSettingsPath));
        }

        [SetUp]
        public void SetUp()
        {
            var environment = mEnvironmentFactory.GetNewEnvironment();

            mEnvironmentByTestId.Add(TestContext.CurrentContext.Test.ID, environment);
        }

        [TearDown]
        public void TearDown()
        {
            if (!mEnvironmentByTestId.ContainsKey(TestContext.CurrentContext.Test.ID))
            {
                return;
            }

            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            mEnvironmentByTestId.Remove(TestContext.CurrentContext.Test.ID);

            environment.Destroy();
        }
    }
}
