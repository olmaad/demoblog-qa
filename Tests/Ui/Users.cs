using DemoBlog.UiTestLib.Environment;
using DemoBlog.UiTestLib.PageComponents;
using DemoBlog.UiTestLib.PageObjects;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace DemoBlog.Tests.Ui
{
    [TestFixture("../../../../TestData/firefoxUiSettings.json")]
    public class Users
    {
        string mEnvironmentSettingsPath;
        EnvironmentFactory mEnvironmentFactory;
        Dictionary<string, TestEnvironment> mEnvironmentByTestId;

        public Users(string environmentSettingsPath)
        {
            mEnvironmentSettingsPath = environmentSettingsPath;
            mEnvironmentByTestId = new Dictionary<string, TestEnvironment>();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            mEnvironmentFactory = new EnvironmentFactory(Path.Combine(TestContext.CurrentContext.TestDirectory, mEnvironmentSettingsPath));
        }

        [SetUp]
        public void SetUp()
        {
            var environment = mEnvironmentFactory.GetNewEnvironment();

            mEnvironmentByTestId.Add(TestContext.CurrentContext.Test.ID, environment);
        }

        [TearDown]
        public void TearDown()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            environment.Destroy();
        }

        [Test]
        public void UserRegister()
        {
            //var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            //var sideMenu = new SideMenuComponent(environment.Driver, environment.Wait);

            //sideMenu.WaitCreated();

            //sideMenu.OpenUserMenu();
            //sideMenu.UserMenu.SwitchToRegister();
            //sideMenu.UserMenu.Register("User", "User", "1234");
            //sideMenu.UserMenu.WaitRegistered();
            //sideMenu.UserMenu.Login("User", "1234");
            //sideMenu.UserMenu.AssertUsername("User");
            //sideMenu.UserMenu.Logout();
            //sideMenu.UserMenu.Close();
        }

        [Test]
        public void RegisterUserNegativeNoData()
        {
            //var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            //var sideMenu = new SideMenuComponent(environment.Driver, environment.Wait);

            //sideMenu.WaitCreated();

            //sideMenu.OpenUserMenu();
            //sideMenu.UserMenu.SwitchToRegister();

            //sideMenu.UserMenu.Register("", "", "");
            //sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Register);
            //sideMenu.UserMenu.ClearRegisterInputs();

            //sideMenu.UserMenu.Register("User", "User", "");
            //sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Register);
            //sideMenu.UserMenu.ClearRegisterInputs();

            //sideMenu.UserMenu.Register("User", "", "User");
            //sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Register);
            //sideMenu.UserMenu.ClearRegisterInputs();

            //sideMenu.UserMenu.Close();
        }

        [Test]
        public void RegisterUserNegativeAlreadyExists()
        {
            //var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            //var sideMenu = new SideMenuComponent(environment.Driver, environment.Wait);

            //sideMenu.WaitCreated();

            //sideMenu.OpenUserMenu();
            //sideMenu.UserMenu.SwitchToRegister();

            //sideMenu.UserMenu.Register("1", "Olma", "1");
            //sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Register);

            //sideMenu.UserMenu.Close();
        }

        [Test]
        public void UserLogin()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            root.SideMenu.OpenUserMenu()
                .Login("1", "1")
                .WaitLoggedIn()
                .AssertUsername("Olma")
                .Logout()
                .Close();
        }

        [Test]
        public void UserRelogin()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            root.SideMenu.OpenUserMenu()
                .Login("1", "1")
                .WaitLoggedIn()
                .AssertUsername("Olma")
                .Logout()
                .AssertPageType(UserMenuComponent.PageType.Login)
                .Login("2", "2")
                .WaitLoggedIn()
                .AssertUsername("Alice")
                .Logout()
                .Close();
        }

        [Test]
        public void UserLoginNegativeNoData()
        {
            //var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            //var sideMenu = new SideMenuComponent(environment.Driver, environment.Wait);

            //sideMenu.WaitCreated();

            //sideMenu.OpenUserMenu();

            //sideMenu.UserMenu.Login("", "");
            //sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Login);
            //sideMenu.UserMenu.ClearLoginInputs();

            //sideMenu.UserMenu.Login("1", "");
            //sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Login);
            //sideMenu.UserMenu.ClearLoginInputs();

            //sideMenu.UserMenu.Login("", "1");
            //sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Login);
            //sideMenu.UserMenu.ClearLoginInputs();

            //sideMenu.UserMenu.Close();
        }

        [Test]
        public void UserLoginNegativeWrongPassword()
        {
            //var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            //var sideMenu = new SideMenuComponent(environment.Driver, environment.Wait);

            //sideMenu.WaitCreated();

            //sideMenu.OpenUserMenu();

            //sideMenu.UserMenu.Login("1", "1234");
            //sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Login);

            //sideMenu.UserMenu.Close();
        }

        [Test]
        public void UserLoginNegativeNoUser()
        {
            //var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            //var sideMenu = new SideMenuComponent(environment.Driver, environment.Wait);

            //sideMenu.WaitCreated();

            //sideMenu.OpenUserMenu();

            //sideMenu.UserMenu.Login("1234", "1");
            //sideMenu.UserMenu.AssertPageType(UserMenuComponent.PageType.Login);

            //sideMenu.UserMenu.Close();
        }
    }
}
