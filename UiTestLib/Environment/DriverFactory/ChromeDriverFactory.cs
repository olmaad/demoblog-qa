using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoBlog.UiTestLib.Environment.DriverFactory
{
    public class ChromeDriverFactory : IDriverFactory
    {
        string mDriverPath;
        string mBrowserPath;

        public ChromeDriverFactory(string driverPath, string browserPath)
        {
            mDriverPath = driverPath;
            mBrowserPath = browserPath;
        }

        public IWebDriver GetNewDriver()
        {
            var service = ChromeDriverService.CreateDefaultService(mDriverPath);

            var driver = new ChromeDriver(service, new ChromeOptions()
            {
                BinaryLocation = mBrowserPath
            });

            return driver;
        }
    }
}
