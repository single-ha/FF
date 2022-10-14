using System.Collections;
using System.Collections.Generic;
using Assets.Script.Config;
using Assets.Script.Manager;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Script.UI
{
    public class MainPanelPresenter:PanelPresenterBase
    {
        protected override void ConfigPanelPresenter(PanelConfig panelConfig)
        {
            panelConfig.panelType = PanelType.Normal;
        }

        protected override void OnShow(PanelDateBase panelDateBase)
        {
            var config = Backgrounds_Config.Inst.Backgrounds;
            StageManager.Inst.MainStage.Decorate("1");
            StageManager.Inst.MainStage.SetCamera("1");
            Sphere s = new Sphere();
            s.SetSphereTemplate("6001",0);
            StageManager.Inst.MainStage.Show(s);
            // var loadingPanel = UIManager.Inst.OpenPanel<LoadingPanelPresenter, LoadingPanelView>();
            // StartCoroutine(Test(loadingPanel));
        }
    }
}