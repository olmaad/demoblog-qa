using DemoBlog.UiTestLib.Resources;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DemoBlog.UiTestLib.Bots
{
    public class RelativeFindBot : IFindBot
    {
        IWebElement mRoot;
        IWebDriver mDriver;
        IWait<IWebDriver> mWait;

        public RelativeFindBot(IWebElement root, IWebDriver driver, IWait<IWebDriver> wait)
        {
            mRoot = root;
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
            return mWait.Until(driver =>
            {
                var element = mRoot.FindElement(locator);

                return element.Displayed ? element : null;
            });
        }

        public IWebElement FindVisible(By locator)
        {
            var element = mRoot.FindElement(locator);

            Assert.That(element.Displayed, Is.True, Strings.ElementNotVisible);

            return element;
        }
    }
}
