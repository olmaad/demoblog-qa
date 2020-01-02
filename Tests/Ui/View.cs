using DemoBlog.UiTestLib.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;

namespace DemoBlog.Tests.Ui
{
    public class View : UiEnvironmentTestFixture
    {
        public View(string environmentSettingsPath) :
            base(environmentSettingsPath)
        { }

        [Test]
        public void TakeMainPageScreenshot()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var posts = new PostsPage(environment).Load();

            var screenshoter = environment.Driver as ITakesScreenshot;

            var screenshot = screenshoter.GetScreenshot();

            var filepath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "screen.png");

            screenshot.SaveAsFile(filepath);

            TestContext.AddTestAttachment(Path.Combine(filepath));
        }
    }
}
