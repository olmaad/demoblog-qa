using DemoBlog.UiTestLib.Resources;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DemoBlog.UiTestLib.Bots
{
    public class FindBot : IFindBot
    {
        IWebDriver mDriver;
        IWait<IWebDriver> mWait;

        public FindBot(IWebDriver driver, IWait<IWebDriver> wait)
        {
            mDriver = driver;
            mWait = wait;
        }

        public IFindBot RelativeTo(By locator)
        {
            var element = FindVisible(locator);

            return new RelativeFindBot(element, mDriver, mWait);
        }

        public IWebElement WaitVisible(By locator)
        {
            return mWait.Until(driver => driver.FindElement(locator));
        }

        public IWebElement FindVisible(By locator)
        {
            var element = mDriver.FindElement(locator);

            if (!element.Displayed)
            {
                throw new ElementNotVisibleException();
            }

            return element;
        }
    }
}
