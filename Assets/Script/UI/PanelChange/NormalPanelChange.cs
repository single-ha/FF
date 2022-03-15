using System;
using Assets.Script.UI.Base;
using UnityEngine;

public class NormalPanelChange : IPanelChange
{
    private static NormalPanelChange _inst;

    public static NormalPanelChange Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = new NormalPanelChange();
            }

            return _inst;
        }
    }

    private NormalPanelChange()
    {
    }

    public bool Change(PanelBase curPanel, PanelBase openPanel,UIDate uiDate)
    {
        try
        {
            if (curPanel != null)
            {
                curPanel.Close(false);
            }
            openPanel.Show(uiDate);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
    }
}