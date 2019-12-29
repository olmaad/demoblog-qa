using DemoBlog.UiTestLib.Environment.DriverFactory;
using OpenQA.Selenium.Support.UI;
using System;

namespace DemoBlog.UiTestLib.Environment
{
    public class EnvironmentFactory
    {
        EnvironmentSettings mSettings;
        IDriverFactory mDriverFactory;

        public EnvironmentFactory(string settingsPath)
        {
            mSettings = EnvironmentSettings.Load(settingsPath);

            switch (mSettings.DriverType)
            {
                case "firefox":
                    {
                        mDriverFactory = new FirefoxDriverFactory(mSettings.DriverPath, mSettings.BrowserExecutablePath, mSettings.Headless);
                        break;
                    }
                case "chrome":
                    {
                        mDriverFactory = new ChromeDriverFactory(mSettings.DriverPath, mSettings.BrowserExecutablePath, mSettings.Headless);
                        break;
                    }
                default:
                    {
                        throw new Exception("Unknown driver type");
                    }
            }
        }

        public TestEnvironment GetNewEnvironment()
        {
            var driver = mDriverFactory.GetNewDriver();
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, mSettings.DriverWaitTimeout));

            return new TestEnvironment(mSettings.BaseUrl, driver, wait);
        }
    }
}
