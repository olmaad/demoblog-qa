using DemoBlogUiLib.PageComponents;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace DemoBlogTests.Ui
{
    [TestFixture]
    public class Users
    {
        protected IWebDriver mDriver;
        protected IWait<IWebDriver> mWaiter;

        [SetUp]
        public void SetUp()
        {
            var workingDir = Directory.GetCurrentDirectory();

            var service = FirefoxDriverService.CreateDefaultService(workingDir);
            service.Host = "::1";

            mDriver = new FirefoxDriver(service, new FirefoxOptions()
            {
                BrowserExecutableLocation = "C:\\Users\\olma\\Downloads\\FirefoxPortable32-71.0\\FirefoxPortable32\\App\\Firefox\\firefox.exe"
            });

            mWaiter = new WebDriverWait(mDriver, new TimeSpan(0, 0, 10));
        }

        [TearDown]
        public void TearDown()
        {
            mDriver.Quit();
        }

        [Test]
        public void UserLogin()
        {
            mDriver.Url = "http://localhost:8080/";

            var sideMenu = new SideMenuComponent(mDriver, mWaiter);

            sideMenu.OpenUserMenu();
            sideMenu.UserMenu.Login("1", "1");
            sideMenu.UserMenu.Logout();
            sideMenu.UserMenu.Close();
        }
    }
}
