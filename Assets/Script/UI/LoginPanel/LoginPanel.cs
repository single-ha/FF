using Assets.Script.UI;
using UnityEngine.UI;

namespace Assets.Script.UI.LoginPanel
{
    public class LoginPanel:PanelBase
    {
        public Button Btn_Start;
        protected override void GetDefault()
        {
            prefabName = "LoginPanel";
        }
        public override void OnInit()
        {
            this.Btn_Start = GetComponent<Button>("Button");
        }
    }
}