using System.Collections;
using System.Collections.Generic;
using Assets.Script.Manager;

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
            StageManager.Inst.MainStage.Decorate("1");
            StageManager.Inst.MainStage.SetCamera("1");
            Sphere s = new Sphere();
            s.SetSphere("20000");
            StageManager.Inst.MainStage.Show(s);
            // s.EnableEditor();
            // var loadingPanel = UIManager.Inst.OpenPanel<LoadingPanelPresenter, LoadingPanelView>();
        }
    }
}