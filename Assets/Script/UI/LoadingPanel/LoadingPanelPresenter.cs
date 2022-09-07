using System.Runtime.CompilerServices;

namespace Assets.Script.UI.LoadingPanel
{
    public class LoadingPanelPresenter:PanelPresenterBase
    {
        private LoadingPanel Panel
        {
            get
            {
                return this.View<LoadingPanel>();
            }
        }
        protected override void ConfigPanelPresenter()
        {
            panelConfig.panelType = PanelType.Sys;
            panelConfig.unique = true;
        }

        public void SetValue(float value)
        {
            this.Panel.SetValue(value);

        }
    }
}