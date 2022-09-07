using System;
using Assets.Script.UI;
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
    public bool Change(PanelPresenterBase curPanelPresenter, PanelPresenterBase openPanelPresenter, PanelDateBase panelDateBase)
    {
        try
        {
            if (curPanelPresenter == openPanelPresenter)
            {
                openPanelPresenter.Show(panelDateBase);
                return true;
            }
            if (curPanelPresenter != null && curPanelPresenter.panelConfig.panelType == PanelType.Normal)
            {
                curPanelPresenter.SetViewVisible(false);
            }
            openPanelPresenter.Show(panelDateBase);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
    }
}