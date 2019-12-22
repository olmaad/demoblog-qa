using OpenQA.Selenium;

namespace DemoBlog.UiTestLib.Environment.DriverFactory
{
    public interface IDriverFactory
    {
        IWebDriver GetNewDriver();
    }
}
