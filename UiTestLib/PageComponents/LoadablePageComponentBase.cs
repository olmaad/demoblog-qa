using DemoBlog.UiTestLib.Bots;
using DemoBlog.UiTestLib.Environment;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DemoBlog.UiTestLib.PageComponents
{
    public abstract class LoadablePageComponentBase<T> : LoadableComponent<T> where T : LoadablePageComponentBase<T>
    {
        protected TestEnvironment mEnvironment;

        protected IWebDriver Driver { get { return mEnvironment.Driver; } }
        protected IWait<IWebDriver> Wait { get { return mEnvironment.Wait; } }
        protected IFindBot FindBot { get { return mEnvironment.FindBot; } }
        protected IActionBot ActionBot { get { return mEnvironment.ActionBot; } }

        protected LoadablePageComponentBase(TestEnvironment environment)
        {
            mEnvironment = environment;
        }
    }
}
