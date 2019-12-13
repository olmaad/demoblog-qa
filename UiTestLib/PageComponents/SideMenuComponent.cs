using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace DemoBlog.UiTestLib.PageComponents
{
    public class SideMenuComponent : PageComponentBase
    {
        public UserMenuComponent UserMenu { get; private set; }

        protected IWebElement GetSideMenuRoot()
        {
            return mDriver.FindElement(By.ClassName("menu-container"));
        }

        protected IWebElement GetUserMenuButton()
        {
            return mDriver.FindElement(By.XPath("//div[contains(@class, 'menu-button')][div[contains(@class, 'icon-user')]]"));
        }

        protected IWebElement GetUserWidget()
        {
            return mDriver.FindElement(By.XPath("//label[text()='Пользователь']"));
        }

        public SideMenuComponent(IWebDriver driver, IWait<IWebDriver> waiter) :
            base(driver, waiter)
        {
            UserMenu = new UserMenuComponent(mDriver, mWaiter);
        }

        public void WaitCreated()
        {
            Wait(GetSideMenuRoot);
        }

        public void OpenUserMenu()
        {
            WaitAndGet(GetUserMenuButton).Click();

            Wait(GetUserWidget);
        }
    }
}
