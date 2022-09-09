using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.UI.LoadingPanel
{
    public class LoadingPanelView:PanelViewBase
    {
        private Transform process;
        private Text tex_Process;
        protected override void GetDefault(ref string prefabName)
        {
            prefabName = "LoadingPanel";
        }
        public override void OnInit()
        {
            // process = GetComponent<Transform>("Image/Tran_Process");
            // tex_Process = GetComponent<Text>("Text");
        }

        public void SetValue(float value)
        {
            var scale = this.process.localScale;
            scale.x = value;
            this.process.localScale = scale;
            this.tex_Process.text = $"{value * 100:00}%";
        }
    }
}