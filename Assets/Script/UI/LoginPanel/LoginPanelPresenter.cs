using System.Collections;
using System.Collections.Generic;
using Assets.Script.UI;
using Assets.Script.UI.LoadingPanel;
using UnityEngine;

namespace Assets.Script.UI.LoginPanel
{
    public class LoginPanelPresenter : PanelPresenterBase
    {
        protected override void ConfigPanelPresenter(PanelConfig panelConfig)
        {
            panelConfig.panelType = PanelType.Normal;
        }

        public override void OnInitView()
        {
            this.View<LoginPanelView>().btn_Start.onClick.AddListener(OnClickStart);
        }

        private void OnClickStart()
        {
            var loadingPanel = UIManager.Inst.OpenPanel<LoadingPanelPresenter,LoadingPanel.LoadingPanelView>();
            StartCoroutine(Test(loadingPanel));
        }

        private int total = 2;
        private IEnumerator Test(LoadingPanelPresenter panel)
        {
            int process = 0;
            panel.SetValue(0);
            float value = 0;
            List<string> loads = new List<string>() { "Stage.prefab","10001.prefab" };
            Stage mainStage = null;
            for (int i = 0; i < loads.Count; i++)
            {
                var obj = ResManager.Inst.Load<GameObject>(loads[i]);
                var root = GameObject.Instantiate(obj);
                switch (i)
                {
                    case 0:
                        mainStage = new Stage(root);
                        break;
                    case 1:
                        mainStage.ShowGameObject(root);
                        break;
                }
                process++;
                value = process / (float)total;
                panel.SetValue(value);
                yield return null;
            }
            UIManager.Inst.OpenPanel<MainPanel.MainPanelPresenter, MainPanel.MainPanelView>();
        }
    }
}
