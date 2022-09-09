using UnityEngine;

namespace Assets.Script.UI
{
    public abstract class PanelViewBase:ViewBase
    {
        private string prefabName;

        public string PrefabName
        {
            get
            {
                return prefabName;
            }
        }
        public void SetPrefab(string prefabName = "")
        {
            if (!string.IsNullOrEmpty(this.prefabName))
            {
                return;
            }
            if (string.IsNullOrEmpty(prefabName))
            {
                GetDefault(ref this.prefabName);
            }
            else
            {
                this.prefabName = prefabName;
            }
        }

        protected abstract void GetDefault(ref string prefabName);
    }
}