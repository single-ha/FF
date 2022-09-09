using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Assets.Script.UI
{
    public abstract class ViewBase
    {
        private ViewMonoBehaviour _viewMonoBehaviour;

        protected GameObject root;

        protected T GetComponent<T>(string path)where T:Component
        {
            return Tool.GetComponent<T>(root, path);
        }
        public void Init(GameObject root)
        {
            this.root = root;
            _viewMonoBehaviour = root.GetComponent<ViewMonoBehaviour>();
            if (_viewMonoBehaviour == null)
            {
                _viewMonoBehaviour = root.AddComponent<ViewMonoBehaviour>();
            }
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

        public bool Visible()
        {
            return this.root.gameObject.activeInHierarchy;
        }

        public void GetObjects()
        {
            Type view_tpye = this.GetType();
            for (int i = 0; i < _viewMonoBehaviour.objs.Count; i++)
            {
                var obj = _viewMonoBehaviour.objs[i];
                if (obj == null)
                {
                    Debug.LogWarning($"{this.root.name}上的_viewMonoBehaviour脚本中的第{i}个元素为空");
                    continue;
                }
                var fieldInfo = view_tpye.GetField(obj.name,BindingFlags.NonPublic | BindingFlags.Public|BindingFlags.Instance);
                if (fieldInfo != null)
                {
                    var value = obj.GetComponent(fieldInfo.FieldType);
                    if (value==null)
                    {
                        
                    }
                    else
                    {
                        fieldInfo.SetValue(this,value);
                    }
                }
                else
                {
                    Debug.LogWarning($"{view_tpye.Name}脚本中没有名字为{obj.name}的字段");
                }
            }
        }
        public Coroutine StartCoroutine(IEnumerator enumerator)
        {
            return _viewMonoBehaviour.StartCoroutine(enumerator);
        }
    }
}