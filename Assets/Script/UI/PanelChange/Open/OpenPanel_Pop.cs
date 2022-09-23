using System.Collections.Generic;
using Assets.Script.Manager;

namespace Assets.Script.UI
{
    public class OpenPanel_Pop:Instance<OpenPanel_Pop>,IOpenPanel
    {
        public bool Open(PanelPresenterBase openPanelPresenter, PanelDateBase panelDateBase)
        {
            openPanelPresenter.Show(panelDateBase);
            UIManager.Inst.AddPop(openPanelPresenter);
            return true;
        }
    }
}