using DemoBlog.UiTestLib.Environment;
using DemoBlog.UiTestLib.PageComponents;

namespace DemoBlog.UiTestLib.PageObjects
{
    public class PostsPage : PageBase<PostsPage>
    {
        DateWidgetComponent mDateWidget;
        PostListComponent mPostList;

        public DateWidgetComponent DateWidget { get { return mDateWidget.Load(); } }

        public PostsPage(TestEnvironment environment) :
            base(environment)
        {
            mDateWidget = new DateWidgetComponent(environment);
            mPostList = new PostListComponent(environment);
        }

        protected override void ExecuteLoad()
        {
            Driver.Url = mEnvironment.BaseUrl + "/posts";

            base.ExecuteLoad();

            mDateWidget.Load();
            mPostList.Load();
        }

        protected override bool EvaluateLoadedStatus()
        {
            return base.EvaluateLoadedStatus() && mDateWidget.IsLoaded && mPostList.IsLoaded;
        }
    }
}
