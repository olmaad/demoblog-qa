using DemoBlog.UiTestLib.Bots;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DemoBlog.UiTestLib.Environment
{
    public class TestEnvironment
    {
        public string BaseUrl { get; private set; }
        public IWebDriver Driver { get; private set; }
        public IWait<IWebDriver> Wait { get; private set; }
        public IFindBot FindBot { get; private set; }
        public IActionBot ActionBot { get; private set; }

        public TestEnvironment(string baseUrl, IWebDriver driver, IWait<IWebDriver> wait)
        {
            BaseUrl = baseUrl;

            Driver = driver;
            Wait = wait;

            FindBot = new FindBot(Driver, Wait);
            ActionBot = new ActionBot();
        }

        public void Destroy()
        {
            Driver.Quit();
        }
    }
}
