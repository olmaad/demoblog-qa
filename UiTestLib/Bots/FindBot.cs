using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace DemoBlog.UiTestLib.Bots
{
    public class FindBot : IFindBot
    {
        ISearchContext mSearchContext;
        IWait<IWebDriver> mWait;

        public FindBot(ISearchContext searchContext, IWait<IWebDriver> wait)
        {
            mSearchContext = searchContext;
            mWait = wait;
        }

        public IFindBot RelativeTo(By locator)
        {
            var element = FindVisible(locator);

            return new FindBot(element, mWait);
        }

        public IWebElement WaitVisible(By locator)
        {
            return mWait.Until(driver => driver.FindElement(locator));
        }

        public IWebElement FindVisible(By locator)
        {
            var element = mSearchContext.FindElement(locator);

            if (!element.Displayed)
            {
                throw new ElementNotVisibleException();
            }

            return element;
        }

        public IEnumerable<IWebElement> FindVisibleMultiple(By locator)
        {
            return mSearchContext.FindElements(locator).Where(e => e.Displayed == true);
        }
    }
}
