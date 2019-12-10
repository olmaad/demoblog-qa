using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBlogUiLib.PageComponents
{
    public class PageComponentBase
    {
        protected IWebDriver mDriver;
        protected IWait<IWebDriver> mWaiter;

        protected PageComponentBase(IWebDriver driver, IWait<IWebDriver> waiter)
        {
            mDriver = driver;
            mWaiter = waiter;
        }

        protected void Wait(Func<IWebElement> getter)
        {
            mWaiter.Until((d) => { return getter(); });
        }

        protected IWebElement WaitAndGet(Func<IWebElement> getter)
        {
            return mWaiter.Until((d) => { return getter(); });
        }
    }
}
