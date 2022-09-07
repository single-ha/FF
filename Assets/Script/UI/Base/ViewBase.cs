using UnityEngine;

namespace Assets.Script.UI
{
    public abstract class ViewBase
    {
        protected GameObject root;

        protected T GetComponent<T>(string path)where T:Component
        {
            return Tool.GetComponent<T>(root, path);
        }
        public void Init(GameObject root)
        {
            this.root = root;
            OnInit();
        }
        public virtual void OnInit()
        {

        }

        public void SetViewVisible(bool visible)
        {
            root.gameObject.SetActive(visible);
        }

        public void Destory()
        {
            GameObject.Destroy(root);
        }
    }
}