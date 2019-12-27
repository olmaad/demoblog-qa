using DemoBlog.TestDataLib.Tools;
using DemoBlog.UiTestLib.PageComponents;
using DemoBlog.UiTestLib.PageObjects;
using NUnit.Framework;

namespace DemoBlog.Tests.Ui
{
    public class Users : UiEnvironmentTestFixture
    {
        public Users(string environmentSettingsPath) :
            base(environmentSettingsPath)
        { }

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
                    .Register(username, username, "password")
                    .WaitRegistered()
                    .Login(username, "password")
                    .AssertUsername(username)
                    .Logout();
            }
        }

        [TestCase("", "", "")]
        [TestCase("User", "User", "")]
        [TestCase("User", "", "User")]
        [TestCase("", "User", "User")]
        public void RegisterUserNegativeNoData(string login, string name, string password)
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                    .SwitchToRegister()
                    .Register(login, name, password)
                    .AssertPageType(UserMenuComponent.PageType.Register);
            }
        }

        [TestCase("1", "Olma", "1")]
        [TestCase("2", "Alice", "2")]
        public void RegisterUserNegativeAlreadyExists(string login, string name, string password)
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                    .SwitchToRegister()
                    .Register(login, name, password)
                    .AssertPageType(UserMenuComponent.PageType.Register);
            }
        }

        [TestCase("1", "1", "Olma")]
        [TestCase("2", "2", "Alice")]
        public void UserLogin(string login, string password, string expectedUsername)
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                    .Login(login, password)
                    .AssertUsername(expectedUsername)
                    .Logout();
            }
        }

        [TestCase("1", "1", "Olma", "2", "2", "Alice")]
        public void UserRelogin(string loginA, string passwordA, string expectedUsernameA, string loginB, string passwordB, string expectedUsernameB)
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                   .Login(loginA, passwordA)
                   .AssertUsername(expectedUsernameA)
                   .Logout()
                   .AssertPageType(UserMenuComponent.PageType.Login)
                   .Login(loginB, passwordB)
                   .AssertUsername(expectedUsernameB)
                   .Logout();
            }
        }

        [TestCase("", "")]
        [TestCase("1", "")]
        [TestCase("", "1")]
        public void UserLoginNegativeNoData(string login, string password)
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                   .Login("", "", false)
                   .AssertPageType(UserMenuComponent.PageType.Login);
            }
        }

        [TestCase("1", "1234")]
        public void UserLoginNegativeWrongPassword(string login, string password)
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                   .Login(login, password, false)
                   .AssertPageType(UserMenuComponent.PageType.Login);
            }
        }

        [TestCase("1234", "1")]
        public void UserLoginNegativeNoUser(string login, string password)
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var root = new RootPage(environment).Load();

            using (var userMenu = root.SideMenu.OpenUserMenu())
            {
                userMenu
                   .Login(login, password, false)
                   .AssertPageType(UserMenuComponent.PageType.Login);
            }
        }
    }
}
