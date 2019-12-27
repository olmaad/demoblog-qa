using DemoBlog.UiTestLib.Environment;
using OpenQA.Selenium;

namespace DemoBlog.UiTestLib.PageComponents
{
    public class PostComponent : LoadablePageComponentBase<PostComponent>
    {
        IWebElement mRoot;

        public PostComponent(IWebElement root, TestEnvironment environment) :
            base(environment)
        {
            mRoot = root;
        }

        protected override void ExecuteLoad()
        {
            throw new System.NotImplementedException();
        }

        protected override bool EvaluateLoadedStatus()
        {
            throw new System.NotImplementedException();
        }
    }
}
