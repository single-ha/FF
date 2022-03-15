using Assets.Script.UI.Base;
using Assets.Script.UI.LoginPanel;
using UnityEngine;

public class LoginPanel : PanelBase
{
    
    public override void CreatView()
    {
        view = new LoginView();
    }

    protected override void CongifPanel()
    {
        panelConfig = new PanelConfig();
        panelConfig.name = "LoginPanel";
        panelConfig.panelType = PanelType.Normal;
    }

    public override void OnInit()
    {
        
    }
}