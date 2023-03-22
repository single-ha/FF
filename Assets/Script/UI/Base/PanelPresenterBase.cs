using System;
using System.Xml;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script.UI
{
    public abstract class PanelPresenterBase:ViewPresenterBase
    {
        private PanelConfig panelConfig;

        public PanelConfig PanelConfig
        {
            get
            {
                return panelConfig;
            }
        }
        protected PanelViewBase panelView;

        /// <summary>
        /// 界面是否可以打开.
        /// </summary>
        public bool canOpen = true;

        public override void Init(Action<ViewPresenterBase> closeAction)
        {
            base.Init(closeAction);
            panelConfig = new PanelConfig();
            ConfigPanelPresenter(panelConfig);
            initViewAction = InitPanel;
        }

        protected virtual void SetPrefabName()
        {
            panelView.SetPrefab();
        }
        private void InitPanel(GameObject patent)
        {
            panelView = _view as PanelViewBase;
            SetPrefabName();
            string name = $"{panelView.PrefabName}.prefab";
            var o = ResManager.Inst.Load<GameObject>(name);
            var obj = GameObject.Instantiate(o, patent.transform);
            GetObjects(obj);
        }

        protected abstract void ConfigPanelPresenter(PanelConfig panelConfig);

        public void DestoryPanel()
        {
            _view.Destory();
            _view = null;
        }
    }

    public class PanelConfig
    {
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