using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DemoBlogUiLib.PageComponents
{
    public class UserMenuComponent : PageComponentBase
    {
        public enum PageType
        {
            Login,
            Register,
            View
        }

        protected IWebElement GetUserMenuRoot()
        {
            return mDriver.FindElement(By.ClassName("user-widget-container"));
        }

        protected IWebElement GetUserLoginRoot()
        {
            return GetUserMenuRoot().FindElement(By.ClassName("user-login-widget-container"));
        }

        protected IWebElement GetUserRegisterRoot()
        {
            return GetUserMenuRoot().FindElement(By.ClassName("user-register-widget-container"));
        }

        protected IWebElement GetUserViewRoot()
        {
            return GetUserMenuRoot().FindElement(By.ClassName("user-view-widget-container"));
        }

        protected IWebElement GetLoginInput()
        {
            return GetUserMenuRoot().FindElement(By.XPath("//input[@placeholder='логин']"));
        }

        protected IWebElement GetNameInput()
        {
            return GetUserMenuRoot().FindElement(By.XPath("//input[@placeholder='имя']"));
        }

        protected IWebElement GetPasswordInput()
        {
            return GetUserMenuRoot().FindElement(By.XPath("//input[@placeholder='пароль']"));
        }

        protected IWebElement GetLoginButton()
        {
            return GetUserMenuRoot().FindElement(By.XPath("//button[text()='Войти']"));
        }

        protected IWebElement GetLogoutButton()
        {
            return GetUserMenuRoot().FindElement(By.XPath("//button[text()='Выйти']"));
        }

        protected IWebElement GetRegisterSwitchButton()
        {
            return GetUserLoginRoot().FindElement(By.XPath("//button[text()='Регистрация']"));
        }

        protected IWebElement GetRegisterSubmitButton()
        {
            return GetUserRegisterRoot().FindElement(By.XPath("//button[text()='Регистрация']"));
        }

        protected IWebElement GetCloseButton()
        {
            return GetUserMenuRoot().FindElement(By.XPath("//div[contains(@class, 'button-close')]"));
        }

        protected IWebElement GetGreetingElement()
        {
            return GetUserViewRoot().FindElement(By.XPath("//*[contains(@class, 'greeting')]"));
        }

        public UserMenuComponent(IWebDriver driver, IWait<IWebDriver> waiter) :
            base(driver, waiter)
        { }

        public void Login(string login, string password)
        {
            GetLoginInput().SendKeys(login);
            GetPasswordInput().SendKeys(password);

            GetLoginButton().Click();
        }

        public void WaitLoggedIn()
        {
            Wait(GetUserViewRoot);
        }

        public void Logout()
        {
            GetLogoutButton().Click();
        }

        public void SwitchToRegister()
        {
            GetRegisterSwitchButton().Click();

            Wait(GetUserRegisterRoot);
        }

        public void Register(string login, string name, string password)
        {
            GetLoginInput().SendKeys(login);
            GetNameInput().SendKeys(name);
            GetPasswordInput().SendKeys(password);

            GetRegisterSubmitButton().Click();
        }

        public void WaitRegistered()
        {
            Wait(GetUserLoginRoot);
        }

        public void ClearLoginInputs()
        {
            ClearInput(GetLoginInput());
            ClearInput(GetPasswordInput());
        }

        public void ClearRegisterInputs()
        {
            ClearInput(GetLoginInput());
            ClearInput(GetNameInput());
            ClearInput(GetPasswordInput());
        }

        public void Close()
        {
            GetCloseButton().Click();
        }

        public void AssertUsername(string expected)
        {
            var greetingElement = GetGreetingElement();

            Assert.That(greetingElement.Text, Is.EqualTo(string.Format("Привет {0}!", expected)), "Неверное имя пользователя");
        }

        public void AssertPageType(PageType pageType)
        {
            switch(pageType)
            {
                case PageType.Login:
                    Assert.That(GetUserLoginRoot(), Is.Not.Null);
                    break;
                case PageType.Register:
                    Assert.That(GetUserRegisterRoot(), Is.Not.Null);
                    break;
                case PageType.View:
                    Assert.That(GetUserViewRoot(), Is.Not.Null);
                    break;
            }
        }
    }
}
