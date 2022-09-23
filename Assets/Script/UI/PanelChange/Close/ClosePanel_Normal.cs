using System;
using System.Collections.Generic;
using Assets.Script.Manager;

namespace Assets.Script.UI
{
    public class ClosePanel_Normal:Instance<ClosePanel_Normal>,IPanelClose
    {
        public bool Close(PanelPresenterBase closePanel)
        {
            if (closePanel != null)
            {
                UIManager.Inst.ClosePoPByHandle(closePanel);
                closePanel.DestoryPanel();
                int index = UIManager.Inst.GetNormalIndex(closePanel);
                var curShow = UIManager.Inst.GetNormalPanel(index - 1);
                UIManager.Inst.SetNormalVisible(curShow,true);
                UIManager.Inst.RemoveNormal(closePanel);
                return true;
            }
            return false;
        }
    }
}