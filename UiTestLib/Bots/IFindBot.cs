using OpenQA.Selenium;

namespace DemoBlog.UiTestLib.Bots
{
    public interface IFindBot
    {
        IFindBot RelativeTo(By locator);
        IWebElement WaitVisible(By locator);
        IWebElement FindVisible(By locator);
    }
}
