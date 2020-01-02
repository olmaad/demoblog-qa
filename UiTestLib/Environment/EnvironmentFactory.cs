using DemoBlog.UiTestLib.Environment.DriverFactory;
using OpenQA.Selenium.Support.UI;
using System;

namespace DemoBlog.UiTestLib.Environment
{
    public class EnvironmentFactory
    {
        EnvironmentSettingsItem mSettings;
        IDriverFactory mDriverFactory;

        public EnvironmentFactory(string settingsPath)
        {
            mSettings = EnvironmentSettingsItem.Load(settingsPath);

            switch (mSettings.DriverType)
            {
                case "firefox":
                    {
                        mDriverFactory = new FirefoxDriverFactory(mSettings.DriverPath, mSettings.BrowserExecutablePath, mSettings.Headless);
                        break;
                    }
                case "remoteFirefox":
                    {
                        mDriverFactory = new FirefoxRemoteDriverFactory(mSettings.RemoteDriverHost, mSettings.RemoteDriverPort, mSettings.Headless);
                        break;
                    }
                case "chrome":
                    {
                        mDriverFactory = new ChromeDriverFactory(mSettings.DriverPath, mSettings.BrowserExecutablePath, mSettings.Headless);
                        break;
                    }
                case "remoteChrome":
                    {
                        mDriverFactory = new ChromeRemoteDriverFactory(mSettings.RemoteDriverHost, mSettings.RemoteDriverPort, mSettings.Headless);
                        break;
                    }
                default:
                    {
                        throw new Exception("Unknown driver type: " + mSettings.DriverType);
                    }
            }
        }

        public TestEnvironment GetNewEnvironment()
        {
            var driver = mDriverFactory.GetNewDriver();

            driver.Manage().Window.Size = new System.Drawing.Size(1280, 720);

            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, mSettings.DriverWaitTimeout));

            return new TestEnvironment(mSettings.BaseUrl, driver, wait);
        }
    }
}
