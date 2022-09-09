
using UnityEngine;

namespace Assets.Script.UI.LoadingPanel
{
    public class LoadingPanelPresenter:PanelPresenterBase
    {
        private LoadingPanelView PanelView
        {
            get
            {
                return this.View<LoadingPanelView>();
            }
        }
        protected override void ConfigPanelPresenter(PanelConfig panelConfig)
        {
            panelConfig.panelType = PanelType.Sys;
            panelConfig.unique = true;
        }

        public void SetValue(float value)
        {
            this.PanelView.SetValue(value);
            if (value>=1)
            {
                Close();
            }
        }
    }
}