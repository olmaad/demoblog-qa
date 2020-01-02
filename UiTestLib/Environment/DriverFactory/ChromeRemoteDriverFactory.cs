using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoBlog.UiTestLib.Environment.DriverFactory
{
    public class ChromeRemoteDriverFactory : RemoteDriverFactory
    {
        public ChromeRemoteDriverFactory(string host, int port, bool headless) :
            base(host, port, headless)
        { }

        protected override DriverOptions CreateOptions()
        {
            var options = new ChromeOptions();

            if (mHeadless)
            {
                options.AddArgument("--headless");
            }

            return options;
        }
    }
}
