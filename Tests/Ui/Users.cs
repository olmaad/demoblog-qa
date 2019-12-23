using DemoBlog.TestDataLib.Tools;
using DemoBlog.UiTestLib.Environment;
using DemoBlog.UiTestLib.PageComponents;
using DemoBlog.UiTestLib.PageObjects;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace DemoBlog.Tests.Ui
{
    [TestFixture("../../../../TestData/firefoxUiSettings.json")]
    [TestFixture("../../../../TestData/chromeUiSettings.json")]
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

            mEnvironmentByTestId.Remove(TestContext.CurrentContext.Test.ID);

            environment.Destroy();
        }

        [Test]
        public void UserRegister()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            var username = DataGenerator.GenerateUsername();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                    .SwitchToRegister()
                    .Register(username, username, "1234")
                    .WaitRegistered()
                    .Login(username, "1234")
                    .AssertUsername(username)
                    .Logout();
            }
        }

        [Test]
        public void RegisterUserNegativeNoData()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                    .SwitchToRegister()
                    .Register("", "", "")
                    .AssertPageType(UserMenuComponent.PageType.Register)
                    .ClearRegisterInputs()
                    .Register("User", "User", "")
                    .AssertPageType(UserMenuComponent.PageType.Register)
                    .ClearRegisterInputs()
                    .Register("User", "", "User")
                    .AssertPageType(UserMenuComponent.PageType.Register)
                    .ClearRegisterInputs()
                    .Register("", "User", "User")
                    .AssertPageType(UserMenuComponent.PageType.Register)
                    .ClearRegisterInputs();
            }
        }

        [Test]
        public void RegisterUserNegativeAlreadyExists()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                    .SwitchToRegister()
                    .Register("1", "Olma", "1")
                    .AssertPageType(UserMenuComponent.PageType.Register);
            }
        }

        [Test]
        public void UserLogin()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                    .Login("1", "1")
                    .AssertUsername("Olma")
                    .Logout();
            }
        }

        [Test]
        public void UserRelogin()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                   .Login("1", "1")
                   .AssertUsername("Olma")
                   .Logout()
                   .AssertPageType(UserMenuComponent.PageType.Login)
                   .Login("2", "2")
                   .AssertUsername("Alice")
                   .Logout();
            }
        }

        [Test]
        public void UserLoginNegativeNoData()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                   .Login("", "", false)
                   .AssertPageType(UserMenuComponent.PageType.Login)
                   .ClearLoginInputs()
                   .Login("1", "", false)
                   .AssertPageType(UserMenuComponent.PageType.Login)
                   .ClearLoginInputs()
                   .Login("", "1", false)
                   .AssertPageType(UserMenuComponent.PageType.Login)
                   .ClearLoginInputs();
            }
        }

        [Test]
        public void UserLoginNegativeWrongPassword()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                   .Login("1", "1234", false)
                   .AssertPageType(UserMenuComponent.PageType.Login);
            }
        }

        [Test]
        public void UserLoginNegativeNoUser()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                   .Login("1234", "1", false)
                   .AssertPageType(UserMenuComponent.PageType.Login);
            }
        }
    }
}
