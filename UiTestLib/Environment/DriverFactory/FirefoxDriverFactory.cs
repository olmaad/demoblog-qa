using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace DemoBlog.UiTestLib.Environment.DriverFactory
{
    public class FirefoxDriverFactory : IDriverFactory
    {
        string mDriverPath;
        string mBrowserPath;
        bool mHeadless;

        public FirefoxDriverFactory(string driverPath, string browserPath, bool headless)
        {
            mDriverPath = driverPath;
            mBrowserPath = browserPath;
            mHeadless = headless;
        }

        public IWebDriver GetNewDriver()
        {
            var service = FirefoxDriverService.CreateDefaultService(mDriverPath);
            service.Host = "::1";

            var options = new FirefoxOptions()
            {
                BrowserExecutableLocation = mBrowserPath
            };

            if (mHeadless)
            {
                options.AddArgument("-headless");
            }

            var driver = new FirefoxDriver(service, options);

            return driver;
        }
    }
}
