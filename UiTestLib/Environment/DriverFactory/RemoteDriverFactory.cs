using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace DemoBlog.UiTestLib.Environment.DriverFactory
{
    public abstract class RemoteDriverFactory : IDriverFactory
    {
        protected readonly string mHost;
        protected readonly int mPort;
        protected readonly bool mHeadless;

        protected abstract DriverOptions CreateOptions();

        public RemoteDriverFactory(string host, int port, bool headless)
        {
            mHost = host;
            mPort = port;
            mHeadless = headless;
        }

        public IWebDriver GetNewDriver()
        {
            var options = CreateOptions();
            
            var uriBuilder = new UriBuilder()
            {
                Scheme = "http",
                Host = mHost,
                Port = mPort,
                Path = "/wd/hub"
            };

            var driver = new RemoteWebDriver(uriBuilder.Uri, options);

            return driver;
        }
    }
}
