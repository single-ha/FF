using Assets.Script.UI.Base;
using UnityEngine;

public interface IPanelChange
{
    bool Change(PanelBase curPanel, PanelBase openPanel, UIDate uiDate);
}