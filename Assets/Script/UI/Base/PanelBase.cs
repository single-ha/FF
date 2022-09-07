using UnityEngine;

namespace Assets.Script.UI
{
    public abstract class PanelBase:ViewBase
    {
        public string prefabName;

        public void SetPrefab(string prefabName = "")
        {
            if (!string.IsNullOrEmpty(this.prefabName))
            {
                return;
            }
            if (string.IsNullOrEmpty(prefabName))
            {
                GetDefault();
            }
            else
            {
                this.prefabName = prefabName;
            }
        }

        protected abstract void GetDefault();
    }
}