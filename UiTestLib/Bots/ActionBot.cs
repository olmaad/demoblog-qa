using System;
using OpenQA.Selenium;

namespace DemoBlog.UiTestLib.Bots
{
    public class ActionBot : IActionBot
    {
        public void ClearInput(IWebElement element)
        {
            // SELENIUM PLS

            element.Click();

            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
        }
    }
}
