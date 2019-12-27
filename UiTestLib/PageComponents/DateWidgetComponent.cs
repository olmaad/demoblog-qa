using DemoBlog.UiTestLib.Environment;
using OpenQA.Selenium;

namespace DemoBlog.UiTestLib.PageComponents
{
    public class DateWidgetComponent : LoadablePageComponentBase<DateWidgetComponent>
    {
        static readonly By mRootLocator = By.ClassName("date-widget-container");

        static readonly By mPreviousButtonLocator = By.XPath("(//div[contains(@class, 'day-item-container')])[1]");
        static readonly By mCurrentButtonLocator = By.XPath("(//div[contains(@class, 'day-item-container')])[2]");
        static readonly By mNextButtonLocator = By.XPath("(//div[contains(@class, 'day-item-container')])[3]");

        public DateWidgetComponent(TestEnvironment environment) :
            base(environment)
        { }

        public DateWidgetComponent GoBack()
        {
            FindBot.FindVisible(mPreviousButtonLocator).Click();

            return this;
        }

        public DateWidgetComponent GoForward()
        {
            FindBot.FindVisible(mNextButtonLocator).Click();

            return this;
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
