using System.Collections;
using System.Collections.Generic;
using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEditor.Build.Content;

namespace Assets.Script.UI
{
    public class MainPanelPresenter:PanelPresenterBase
    {
        protected override void ConfigPanelPresenter(PanelConfig panelConfig)
        {
            panelConfig.panelType = PanelType.Normal;
        }

        protected override ViewBase CreatViewInstance()
        {
            return new MainPanelView();
        }

        protected override void OnShow(PanelDateBase panelDateBase)
        {
            StageManager.Inst.MainStage.Decorate(MainInfo.Inst.mainBackground);
            Scene scene = new Scene();
            Sphere s = new Sphere();
            s.SetSphere(SphereInfos.Inst.Shpheres[MainInfo.Inst.mainSphere].SphereTemplate);
            scene.AddSphere(0,s);
            StageManager.Inst.MainStage.Show(s);
            StageManager.Inst.MainStage.SetCamera("1");

            // s.EnableEditor();
            // var loadingPanel = UIManager.Inst.OpenPanel<LoadingPanelPresenter, LoadingPanelView>();
        }
    }
}