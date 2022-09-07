using Assets.Script.UI;
using UnityEngine;

public interface IPanelChange
{
    bool Change(PanelPresenterBase curPanelPresenter, PanelPresenterBase openPanelPresenter, PanelDateBase panelDateBase);
}