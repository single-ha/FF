using Assets.Script.UI;
using UnityEngine.UI;

namespace Assets.Script.UI
{
    public class LoginPanelView:PanelViewBase
    {
        public Button btn_Start;
        protected override void GetDefault(ref string prefabName)
        {
            prefabName = "LoginPanel";
        }
        public override void OnInit()
        {
            // this.btn_Start = GetComponent<Button>("Button");
        }
    }
}