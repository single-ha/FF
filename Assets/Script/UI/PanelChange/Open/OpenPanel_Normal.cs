using System;
using System.Collections.Generic;
using Assets.Script.Manager;
using Assets.Script.UI;
using UnityEngine;

public class OpenPanel_Normal : Instance<OpenPanel_Normal>,IOpenPanel
{
    public bool Open(PanelPresenterBase openPanelPresenter, PanelDateBase panelDateBase)
    {
        var top = UIManager.Inst.GetTopNormal();
        if (top != null)
        {
            UIManager.Inst.SetNormalVisible(top,false);
        }
        openPanelPresenter.Show(panelDateBase);
        UIManager.Inst.AddNormal(openPanelPresenter);
        return true;
    }
}