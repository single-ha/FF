using System;
using System.Collections.Generic;
using Assets.Script.Manager;

namespace Assets.Script.UI
{
    public class OpenPanel_Sys:Instance<OpenPanel_Sys>,IOpenPanel
    {
        public bool Open(PanelPresenterBase openPanelPresenter, PanelDateBase panelDateBase)
        {
            openPanelPresenter.Show(panelDateBase);
            UIManager.Inst.AddSys(openPanelPresenter);
            return true;
        }
    }
}