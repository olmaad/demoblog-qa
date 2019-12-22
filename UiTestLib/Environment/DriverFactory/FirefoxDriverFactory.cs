using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace DemoBlog.UiTestLib.Environment.DriverFactory
{
    public class FirefoxDriverFactory : IDriverFactory
    {
        string mDriverPath;
        string mBrowserPath;

        public FirefoxDriverFactory(string driverPath, string browserPath)
        {
            mDriverPath = driverPath;
            mBrowserPath = browserPath;
        }

        public IWebDriver GetNewDriver()
        {
            var service = FirefoxDriverService.CreateDefaultService(mDriverPath);
            service.Host = "::1";

            var driver = new FirefoxDriver(service, new FirefoxOptions()
            {
                BrowserExecutableLocation = mBrowserPath
            });

            return driver;
        }
    }
}
