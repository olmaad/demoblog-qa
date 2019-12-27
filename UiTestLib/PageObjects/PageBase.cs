using DemoBlog.UiTestLib.Environment;
using DemoBlog.UiTestLib.PageComponents;

namespace DemoBlog.UiTestLib.PageObjects
{
    public abstract class PageBase<T> : LoadablePageComponentBase<T> where T : PageBase<T>
    {
        protected SideMenuComponent mSideMenu;

        public SideMenuComponent SideMenu { get { return mSideMenu.Load(); } }

        protected PageBase(TestEnvironment environment) :
            base(environment)
        {
            mSideMenu = new SideMenuComponent(environment);
        }

        protected override void ExecuteLoad()
        {
            mSideMenu.Load();
        }

        protected override bool EvaluateLoadedStatus()
        {
            return mSideMenu.IsLoaded;
        }
    }
}
