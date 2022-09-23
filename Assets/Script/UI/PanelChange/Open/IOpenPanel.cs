using System.Collections.Generic;
using Assets.Script.UI;
using UnityEngine;

public interface IOpenPanel
{
    bool Open(PanelPresenterBase openPanelPresenter, PanelDateBase panelDateBase);
}