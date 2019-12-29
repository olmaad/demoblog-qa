using DemoBlog.UiTestLib.Environment;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace DemoBlog.Tests.Ui
{
    [TestFixture("../../../../TestData/firefoxUiSettings.json")]
    [TestFixture("../../../../TestData/chromeUiSettings.json")]
    [TestFixture("../../../../TestData/headlessChromeUiSettings.json")]
    [TestFixture("../../../../TestData/headlessFirefoxUiSettings.json")]
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
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            mEnvironmentByTestId.Remove(TestContext.CurrentContext.Test.ID);

            environment.Destroy();
        }
    }
}
