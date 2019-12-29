using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoBlog.UiTestLib.Environment.DriverFactory
{
    public class ChromeDriverFactory : IDriverFactory
    {
        string mDriverPath;
        string mBrowserPath;
        bool mHeadless;

        public ChromeDriverFactory(string driverPath, string browserPath, bool headless)
        {
            mDriverPath = driverPath;
            mBrowserPath = browserPath;
            mHeadless = headless;
        }

        public IWebDriver GetNewDriver()
        {
            var service = ChromeDriverService.CreateDefaultService(mDriverPath);

            var options = new ChromeOptions()
            {
                BinaryLocation = mBrowserPath
            };

            if (mHeadless)
            {
                options.AddArgument("--headless");
            }

            var driver = new ChromeDriver(service, options);

            return driver;
        }
    }
}
