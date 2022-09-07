using System.Collections;
using Assets.Script.UI;
using Assets.Script.UI.LoadingPanel;
using UnityEngine;

namespace Assets.Script.UI.LoginPanel
{
    public class LoginPanelPresenter : PanelPresenterBase
    {
        protected override void ConfigPanelPresenter()
        {
            panelConfig.panelType = PanelType.Normal;
        }

        public override void OnInitView()
        {
            this.View<LoginPanel>().Btn_Start.onClick.AddListener(OnClickStart);
        }

        private void OnClickStart()
        {
            var loadingPanel = UIManager.Inst.OpenPanel<LoadingPanelPresenter,LoadingPanel.LoadingPanel>();
            GameMain.Inst.StartCoroutine(Test(loadingPanel));
        }

        private IEnumerator Test(LoadingPanelPresenter panel)
        {
            panel.SetValue(0);
            float value = 0;
            while (value<=1)
            {
                panel.SetValue(value);
                value +=Time.deltaTime;
                yield return null;
            }
            panel.Close();
        }
    }
}
