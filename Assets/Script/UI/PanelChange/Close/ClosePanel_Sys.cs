using System;
using System.Collections.Generic;
using Assets.Script.Manager;

namespace Assets.Script.UI
{
    public class ClosePanel_Sys:Instance<ClosePanel_Sys>,IPanelClose
    {
        public bool Close(PanelPresenterBase closePanel)
        {
            if (closePanel!=null)
            {
                closePanel.DestoryPanel();
                UIManager.Inst.RemoveSys(closePanel);
                return true;
            }

            return false;
        }
    }
}