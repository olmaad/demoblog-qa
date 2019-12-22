using OpenQA.Selenium;

namespace DemoBlog.UiTestLib.Bots
{
    public interface IActionBot
    {
        void ClearInput(IWebElement locator);
    }
}
