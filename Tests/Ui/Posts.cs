using DemoBlog.UiTestLib.PageObjects;
using NUnit.Framework;

namespace DemoBlog.Tests.Ui
{
    public class Posts : UiEnvironmentTestFixture
    {
        public Posts(string environmentSettingsPath) :
            base(environmentSettingsPath)
        { }

        [Test]
        public void fgfg()
        {
            var environment = mEnvironmentByTestId[TestContext.CurrentContext.Test.ID];

            var posts = new PostsPage(environment).Load();

            posts.DateWidget
                .GoBack()
                .GoForward();
        }
    }
}
