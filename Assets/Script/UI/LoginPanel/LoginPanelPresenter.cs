using System.Collections;
using System.Collections.Generic;
using Assets.Script.Manager;
using Assets.Script.UI;
using UnityEngine;

namespace Assets.Script.UI
{
    public class LoginPanelPresenter : PanelPresenterBase
    {
        protected override void ConfigPanelPresenter(PanelConfig panelConfig)
        {
            panelConfig.panelType = PanelType.Normal;
        }

        protected override ViewBase CreatViewInstance()
        {
            return new LoginPanelView();
        }

        public override void OnInitView()
        {
            this.View<LoginPanelView>().btn_Start.onClick.AddListener(OnClickStart);
        }

        private void OnClickStart()
        {
            UIManager.Inst.OpenPanel<MainPanelPresenter>();
        }
    }
}
