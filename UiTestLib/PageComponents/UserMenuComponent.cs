using DemoBlog.UiTestLib.Environment;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace DemoBlog.UiTestLib.PageComponents
{
    public class UserMenuComponent : LoadablePageComponentBase<UserMenuComponent>, IDisposable
    {
        public enum PageType
        {
            Login,
            Register,
            View
        }

        static readonly By mRootLocator = By.ClassName("user-widget-container");
        static readonly By mUserLoginRootLocator = By.ClassName("user-login-widget-container");
        static readonly By mUserRegisterRootLocator = By.ClassName("user-register-widget-container");
        static readonly By mUserViewRootLocator = By.ClassName("user-view-widget-container");

        static readonly By mLoginInputLocator = By.XPath("//input[@placeholder='login']");
        static readonly By mNameInputLocator = By.XPath("//input[@placeholder='name']");
        static readonly By mPasswordInputLocator = By.XPath("//input[@placeholder='password']");
        static readonly By mLoginButtonLocator = By.XPath("//button[text()='Sign in']");
        static readonly By mLogoutButtonLocator = By.XPath("//button[text()='Sign out']");
        static readonly By mRegisterSwitchButtonLocator = By.XPath("//button[text()='Sign up']");
        static readonly By mRegisterSubmitButtonLocator = By.XPath("//button[text()='Sign up']");
        static readonly By mCloseButtonLocator = By.XPath("//div[contains(@class, 'button-close')]");
        static readonly By mGreetingElementLocator = By.XPath("//*[contains(@class, 'greeting')]");

        public UserMenuComponent(TestEnvironment environment) :
            base(environment)
        { }

        public UserMenuComponent Login(string login, string password, bool wait = true)
        {
            var rootFindBot = FindBot.RelativeTo(mUserLoginRootLocator);

            rootFindBot.FindVisible(mLoginInputLocator).SendKeys(login);
            rootFindBot.FindVisible(mPasswordInputLocator).SendKeys(password);

            rootFindBot.FindVisible(mLoginButtonLocator).Submit();

            if (wait)
            {
                FindBot.WaitVisible(mUserViewRootLocator);
            }

            return this;
        }

        public UserMenuComponent Logout()
        {
            var rootFindBot = FindBot.RelativeTo(mUserViewRootLocator);

            rootFindBot.FindVisible(mLogoutButtonLocator).Submit();

            return this;
        }

        public UserMenuComponent SwitchToRegister()
        {
            FindBot.RelativeTo(mUserLoginRootLocator).FindVisible(mRegisterSwitchButtonLocator).Click();

            FindBot.WaitVisible(mUserRegisterRootLocator);

            return this;
        }

        public UserMenuComponent Register(string login, string name, string password)
        {
            var rootFindBot = FindBot.RelativeTo(mUserRegisterRootLocator);

            rootFindBot.FindVisible(mLoginInputLocator).SendKeys(login);
            rootFindBot.FindVisible(mNameInputLocator).SendKeys(name);
            rootFindBot.FindVisible(mPasswordInputLocator).SendKeys(password);

            rootFindBot.FindVisible(mRegisterSubmitButtonLocator).Click();

            return this;
        }

        public UserMenuComponent WaitRegistered()
        {
            FindBot.WaitVisible(mUserLoginRootLocator);

            return this;
        }

        public UserMenuComponent ClearLoginInputs()
        {
            var rootFindBot = FindBot.RelativeTo(mUserLoginRootLocator);

            ActionBot.ClearInput(rootFindBot.FindVisible(mLoginInputLocator));
            ActionBot.ClearInput(rootFindBot.FindVisible(mPasswordInputLocator));

            return this;
        }

        public UserMenuComponent ClearRegisterInputs()
        {
            var rootFindBot = FindBot.RelativeTo(mUserRegisterRootLocator);

            ActionBot.ClearInput(rootFindBot.FindVisible(mLoginInputLocator));
            ActionBot.ClearInput(rootFindBot.FindVisible(mNameInputLocator));
            ActionBot.ClearInput(rootFindBot.FindVisible(mPasswordInputLocator));

            return this;
        }

        public UserMenuComponent Close()
        {
            FindBot.RelativeTo(mRootLocator).FindVisible(mCloseButtonLocator).Click();

            return this;
        }

        public UserMenuComponent AssertUsername(string expected)
        {
            var rootFindBot = FindBot.RelativeTo(mUserViewRootLocator);

            var greetingElement = rootFindBot.FindVisible(mGreetingElementLocator);

            Assert.That(greetingElement.Text, Is.EqualTo(string.Format("Hello {0}!", expected)), "Неверное имя пользователя");

            return this;
        }

        public UserMenuComponent AssertPageType(PageType pageType)
        {
            switch(pageType)
            {
                case PageType.Login:
                    FindBot.FindVisible(mUserLoginRootLocator);
                    break;
                case PageType.Register:
                    FindBot.FindVisible(mUserRegisterRootLocator);
                    break;
                case PageType.View:
                    FindBot.FindVisible(mUserViewRootLocator);
                    break;
            }

            return this;
        }

        public void Dispose()
        {
            try
            {
                Close();
            }
            catch { }
        }

        protected override void ExecuteLoad()
        {
            FindBot.WaitVisible(mRootLocator);
        }

        protected override bool EvaluateLoadedStatus()
        {
            try
            {
                FindBot.FindVisible(mRootLocator);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
