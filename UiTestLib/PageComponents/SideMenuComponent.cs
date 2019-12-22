using DemoBlog.UiTestLib.Environment;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace DemoBlog.UiTestLib.PageComponents
{
    public class SideMenuComponent : LoadablePageComponentBase<SideMenuComponent>
    {
        static readonly By mRootLocator = By.ClassName("menu-container");

        static readonly By mUserMenuButtonLocator = By.XPath("//div[contains(@class, 'menu-button')][div[contains(@class, 'icon-user')]]");

        UserMenuComponent mUserMenu;

        public new bool IsLoaded { get { return base.IsLoaded; } }

        public SideMenuComponent(TestEnvironment environment) :
            base(environment)
        {
            mUserMenu = new UserMenuComponent(environment);
        }

        public UserMenuComponent OpenUserMenu()
        {
            FindBot.FindVisible(mUserMenuButtonLocator).Click();

            return mUserMenu.Load();
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
