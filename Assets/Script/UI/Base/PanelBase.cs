using System;
using System.Xml;
using UnityEngine;

namespace Assets.Script.UI.Base
{
    public abstract class PanelBase
    {
        public PanelConfig panelConfig;
        public ViewBase view;
        protected Action<bool> PreClose_Action;
        protected Action<bool> AfterClose_Action;
        /// <summary>
        /// 界面是否可以打开
        /// </summary>
        public bool canOpen = true;

        protected PanelBase()
        {
            CongifPanel();
        }

        protected abstract void CongifPanel();
        public virtual void PreShow()
        {
        }
        public void Init(GameObject root)
        {
            CreatView();
            view.Init();
            view.root = root;
            PreClose_Action = PreClose;
            AfterClose_Action = AfterClose;
            OnInit();
        }

        public virtual void OnInit()
        {

        }
        public abstract void CreatView();

        public void Show(UIDate uiDate)
        {
            SetViewVisible(true);
            OnShow(uiDate);
        }

        protected virtual void OnShow(UIDate uiDate)
        {

        }
        public void ReturnPanel(params object[] datas)
        {
            SetViewVisible(true);
        }
        public void SetViewVisible(bool visible)
        {
            view.root.gameObject.SetActive(visible);
        }
        protected virtual void PreClose(bool destory = true)
        {

        }
        public virtual void Close(bool destory = true)
        {
            PreClose_Action?.Invoke(destory);
            if (destory)
            {
                UIManager.Inst.ClosePanle(this);
            }
            else
            {
                SetViewVisible(false);
            }
            AfterClose_Action?.Invoke(destory);
        }
        protected virtual void AfterClose(bool destory = true)
        {

        }

        public void Destory()
        {
            GameObject.Destroy(view.root);
            view = null;
        }



    }

    public class PanelConfig
    {
        public string name;
        /// <summary>
        /// 打开界面的时候界面位置
        /// </summary,-1标识在末尾添加
        public int index=-1;
        public PanelType panelType;
    }

    public enum PanelType
    {
        Normal,
        PoP,
        Sys
    }

    public class UIDate
    {
    }
}