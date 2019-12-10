using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBlogUiLib.PageComponents
{
    public class UserMenuComponent : PageComponentBase
    {
        protected IWebElement GetLoginInput()
        {
            return mDriver.FindElement(By.XPath("//input[@placeholder='логин']"));
        }

        protected IWebElement GetPasswordInput()
        {
            return mDriver.FindElement(By.XPath("//input[@placeholder='пароль']"));
        }

        protected IWebElement GetLoginButton()
        {
            return mDriver.FindElement(By.XPath("//button[text()='Войти']"));
        }

        protected IWebElement GetLogoutButton()
        {
            return mDriver.FindElement(By.XPath("//button[text()='Выйти']"));
        }

        protected IWebElement GetCloseButton()
        {
            return mDriver.FindElement(By.XPath("//div[contains(@class, 'button-close')]"));
        }

        public UserMenuComponent(IWebDriver driver, IWait<IWebDriver> waiter) :
            base(driver, waiter)
        { }

        public void Login(string login, string password)
        {
            GetLoginInput().SendKeys(login);
            GetPasswordInput().SendKeys(password);

            GetLoginButton().Click();

            Wait(GetLogoutButton);
        }

        public void AssertUsername(string expected)
        {

        }

        public void Logout()
        {
            GetLogoutButton().Click();

            Wait(GetLoginButton);
        }

        public void Close()
        {
            GetCloseButton().Click();
        }
    }
}
