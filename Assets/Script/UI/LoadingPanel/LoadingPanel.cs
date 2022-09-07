using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.UI.LoadingPanel
{
    public class LoadingPanel:PanelBase
    {
        public Transform process;
        public Text Tex_Process;
        protected override void GetDefault()
        {
            prefabName = "LoadingPanel";
        }
        public override void OnInit()
        {
            process = GetComponent<Transform>("Image/Tran_Process");
            Tex_Process = GetComponent<Text>("Text");
        }


        public void SetValue(float value)
        {
            var scale = this.process.localScale;
            scale.x = value;
            this.process.localScale = scale;
            this.Tex_Process.text = $"{value * 100:00}%";
        }
    }
}