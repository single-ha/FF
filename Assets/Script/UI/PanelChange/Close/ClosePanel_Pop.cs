using System;
using System.Collections.Generic;
using Assets.Script.Manager;

namespace Assets.Script.UI
{
    public class ClosePanel_Pop :Instance<ClosePanel_Pop>,IPanelClose
    {
        public bool Close(PanelPresenterBase closePanel)
        {
            if (closePanel != null)
            {
                closePanel.DestoryPanel();
                UIManager.Inst.RemovePop(closePanel);
                return true;
            }
            return false;
        }
    }
}