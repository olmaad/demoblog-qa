using DemoBlog.UiTestLib.Environment;
using DemoBlog.UiTestLib.PageComponents;

namespace DemoBlog.UiTestLib.PageObjects
{
    public class RootPage : LoadablePageComponentBase<RootPage>
    {
        SideMenuComponent mSideMenu;

        public SideMenuComponent SideMenu { get { return mSideMenu.Load(); } }

        public RootPage(TestEnvironment environment) :
            base(environment)
        {
            mSideMenu = new SideMenuComponent(environment);
        }

        protected override void ExecuteLoad()
        {
            Driver.Url = mEnvironment.BaseUrl;

            mSideMenu.Load();
        }

        protected override bool EvaluateLoadedStatus()
        {
            return mSideMenu.IsLoaded;
        }
    }
}
