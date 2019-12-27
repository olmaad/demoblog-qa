using OpenQA.Selenium;
using System.Collections.Generic;

namespace DemoBlog.UiTestLib.Bots
{
    public interface IFindBot
    {
        IFindBot RelativeTo(By locator);
        IWebElement WaitVisible(By locator);
        IWebElement FindVisible(By locator);
        IEnumerable<IWebElement> FindVisibleMultiple(By locator);
    }
}
