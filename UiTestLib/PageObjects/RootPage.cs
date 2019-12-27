using DemoBlog.UiTestLib.Environment;
using DemoBlog.UiTestLib.PageComponents;

namespace DemoBlog.UiTestLib.PageObjects
{
    public class RootPage : PageBase<RootPage>
    {
        public RootPage(TestEnvironment environment) :
            base(environment)
        { }

        protected override void ExecuteLoad()
        {
            Driver.Url = mEnvironment.BaseUrl;

            base.ExecuteLoad();
        }

        protected override bool EvaluateLoadedStatus()
        {
            return mSideMenu.IsLoaded && base.EvaluateLoadedStatus();
        }
    }
}
