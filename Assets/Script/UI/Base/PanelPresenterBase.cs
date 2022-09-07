using System;
using System.Xml;
using UnityEngine;

namespace Assets.Script.UI
{
    public abstract class PanelPresenterBase:ViewPresenterBase
    {
        public PanelConfig panelConfig;
        private Action<PanelPresenterBase> closeAction;
        protected PanelBase panelView;
        /// <summary>
        /// 界面是否可以打开.
        /// </summary>
        public bool canOpen = true;

        public void Init(Action<PanelPresenterBase> closeAction)
        {
            panelConfig = new PanelConfig();
            ConfigPanelPresenter();
            this.closeAction = closeAction;
            Init();
        }

        protected virtual void SetPrefabName()
        {
            panelView.SetPrefab();
        }

        public void InitPanel<T>(GameObject patent)where T:PanelBase,new()
        {
            _view = new T();
            panelView=_view as PanelBase;
            SetPrefabName();
            string name = $"{panelView.prefabName}.prefab";
            var o = ResManager.Inst.Load<GameObject>(name);
            var obj = GameObject.Instantiate(o, patent.transform);
            InitView(obj);
        }
        protected abstract void ConfigPanelPresenter();
        public override void Close()
        {
            PreClose();
            closeAction?.Invoke( this);
            AfterClose();
            this.date?.OnClose?.Invoke();
        }

        public void DestoryPanel()
        {
            _view.Destory();
            _view = null;
        }
    }

    public class PanelConfig
    {
        public string name;
        public PanelType panelType;
        public bool unique;

    }

    public enum PanelType
    {
        Normal,
        PoP,
        Sys
    }

    public class PanelDateBase
    {
        public Action OnClose;
    }
}