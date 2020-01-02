using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace DemoBlog.UiTestLib.Environment.DriverFactory
{
    public class FirefoxRemoteDriverFactory : RemoteDriverFactory
    {
        public FirefoxRemoteDriverFactory(string host, int port, bool headless) :
            base(host, port, headless)
        { }

        protected override DriverOptions CreateOptions()
        {
            var options = new FirefoxOptions();

            if (mHeadless)
            {
                options.AddArgument("-headless");
            }

            return options;
        }
    }
}
