using DemoBlog.UiTestLib.PageComponents;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace DemoBlog.Tests.Ui
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

            mWaiter = new WebDriverWait(mDriver, new TimeSpan(0, 0, 3));

            mDriver.Url = "http://localhost:8080/";
        }

        [TearDown]
        public void TearDown()
        {
            mDriver.Quit();
        }

        [Test]
        public void UserRegister()
        {
            var sideMenu = new SideMenuComponent(mDriver, mWaiter);

            sideMenu.WaitCreated();

            sideMenu.OpenUserMenu();
            sideMenu.UserMenu.SwitchToRegister();
            sideMenu.UserMenu.Register("User", "User", "1234");
            sideMenu.UserMenu.WaitRegistered();
            sideMenu.UserMenu.Login("User", "1234");
            sideMenu.UserMenu.AssertUsername("User");
            sideMenu.UserMenu.Logout();
            sideMenu.UserMenu.Close();
        }

        [Test]
        public void RegisterUserNegativeNoData()
        {
            var sideMenu = new SideMenuComponent(mDriver, mWaiter);

            sideMenu.WaitCreated();

            sideMenu.OpenUserMenu();
            sideMenu.UserMenu.SwitchToRegister();

            sideMenu.UserMenu.Register("", "", "");
            sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Register);
            sideMenu.UserMenu.ClearRegisterInputs();

            sideMenu.UserMenu.Register("User", "User", "");
            sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Register);
            sideMenu.UserMenu.ClearRegisterInputs();

            sideMenu.UserMenu.Register("User", "", "User");
            sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Register);
            sideMenu.UserMenu.ClearRegisterInputs();

            sideMenu.UserMenu.Close();
        }

        [Test]
        public void RegisterUserNegativeAlreadyExists()
        {
            var sideMenu = new SideMenuComponent(mDriver, mWaiter);

            sideMenu.WaitCreated();

            sideMenu.OpenUserMenu();
            sideMenu.UserMenu.SwitchToRegister();

            sideMenu.UserMenu.Register("1", "Olma", "1");
            sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Register);

            sideMenu.UserMenu.Close();
        }

        [Test]
        public void UserLogin()
        {
            var sideMenu = new SideMenuComponent(mDriver, mWaiter);

            sideMenu.WaitCreated();

            sideMenu.OpenUserMenu();
            sideMenu.UserMenu.Login("1", "1");
            sideMenu.UserMenu.WaitLoggedIn();
            sideMenu.UserMenu.AssertUsername("Olma");
            sideMenu.UserMenu.Logout();
            sideMenu.UserMenu.Close();
        }

        [Test]
        public void UserRelogin()
        {
            var sideMenu = new SideMenuComponent(mDriver, mWaiter);

            sideMenu.WaitCreated();

            sideMenu.OpenUserMenu();
            sideMenu.UserMenu.Login("1", "1");
            sideMenu.UserMenu.WaitLoggedIn();
            sideMenu.UserMenu.AssertUsername("Olma");
            sideMenu.UserMenu.Logout();
            sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Login);
            sideMenu.UserMenu.Login("2", "2");
            sideMenu.UserMenu.WaitLoggedIn();
            sideMenu.UserMenu.AssertUsername("Alice");
            sideMenu.UserMenu.Logout();
            sideMenu.UserMenu.Close();
        }

        [Test]
        public void UserLoginNegativeNoData()
        {
            var sideMenu = new SideMenuComponent(mDriver, mWaiter);

            sideMenu.WaitCreated();

            sideMenu.OpenUserMenu();

            sideMenu.UserMenu.Login("", "");
            sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Login);
            sideMenu.UserMenu.ClearLoginInputs();

            sideMenu.UserMenu.Login("1", "");
            sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Login);
            sideMenu.UserMenu.ClearLoginInputs();

            sideMenu.UserMenu.Login("", "1");
            sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Login);
            sideMenu.UserMenu.ClearLoginInputs();

            sideMenu.UserMenu.Close();
        }

        [Test]
        public void UserLoginNegativeWrongPassword()
        {
            var sideMenu = new SideMenuComponent(mDriver, mWaiter);

            sideMenu.WaitCreated();

            sideMenu.OpenUserMenu();

            sideMenu.UserMenu.Login("1", "1234");
            sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Login);

            sideMenu.UserMenu.Close();
        }

        [Test]
        public void UserLoginNegativeNoUser()
        {
            var sideMenu = new SideMenuComponent(mDriver, mWaiter);

            sideMenu.WaitCreated();

            sideMenu.OpenUserMenu();

            sideMenu.UserMenu.Login("1234", "1");
            sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Login);

            sideMenu.UserMenu.Close();
        }
    }
}
