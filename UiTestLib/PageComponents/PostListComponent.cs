using DemoBlog.UiTestLib.Environment;
using OpenQA.Selenium;
using System.Linq;

namespace DemoBlog.UiTestLib.PageComponents
{
    public class PostListComponent : LoadablePageComponentBase<PostListComponent>
    {
        static readonly By mRootLocator = By.ClassName("post-list");

        static readonly By mPostLocator = By.ClassName("post");

        public PostComponent this[int index]
        {
            get
            {
                var postElements = FindBot.RelativeTo(mRootLocator).FindVisibleMultiple(mPostLocator);

                return new PostComponent(postElements.ElementAt(index), mEnvironment);
            }
        }

        public PostListComponent(TestEnvironment environment) :
            base(environment)
        { }

        public int GetPostCount()
        {
            return FindBot.RelativeTo(mRootLocator).FindVisibleMultiple(mPostLocator).Count();
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
