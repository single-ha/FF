using System.Collections;
using System.Collections.Generic;
using Assets.Script.Config;
using UnityEngine;

namespace Assets.Script.UI
{
    public class MainPanelPresenter:PanelPresenterBase
    {
        private Stage mainStage;
        protected override void ConfigPanelPresenter(PanelConfig panelConfig)
        {
            panelConfig.panelType = PanelType.Normal;
        }

        protected override void OnShow(PanelDateBase panelDateBase)
        {
            mainStage = new Stage();
            var config = GameConfig.BackGround;
            mainStage.Decorate("1");
            var loadingPanel = UIManager.Inst.OpenPanel<LoadingPanelPresenter, LoadingPanelView>();
            StartCoroutine(Test(loadingPanel));
        }
        private IEnumerator Test(LoadingPanelPresenter panel)
        {
            Debuger.Log("111");
            int process = 0;
            panel.SetValue(0);
            float value = 0;
            List<string> loads = new List<string>() {"ground_0.prefab" };
            for (int i = 0; i < loads.Count; i++)
            {
                var obj = ResManager.Inst.Load<GameObject>(loads[i]);
                var root = GameObject.Instantiate(obj);
                mainStage.ShowGameObject(root);
                process++;
                value = process / (float)2;
                panel.SetValue(value);
                yield return null;
            }
            panel.LoadComplate();
        }
    }
}