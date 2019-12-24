using DemoBlog.UiTestLib.Environment;

namespace DemoBlog.UiTestLib.PageObjects
{
    public class PostsPage : PageBase<PostsPage>
    {
        public PostsPage(TestEnvironment environment) :
            base(environment)
        { }

        protected override void ExecuteLoad()
        {
            Driver.Url = mEnvironment.BaseUrl + "/posts";

            base.ExecuteLoad();
        }

        protected override bool EvaluateLoadedStatus()
        {
            return base.EvaluateLoadedStatus();
        }
    }
}
