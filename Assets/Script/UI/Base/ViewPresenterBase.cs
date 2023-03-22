using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Script.UI
{
    public abstract class ViewPresenterBase
    {
        protected Action<GameObject> initViewAction;
        protected Action<ViewPresenterBase> closeAction;
        protected ViewBase _view;
        protected PanelDateBase date;


        protected T View<T>() where T : ViewBase, new()
        {
            if (_view == null)
            {
                _view = new T();
            }

            return (T)_view;
        }

        protected abstract ViewBase CreatViewInstance();
        public virtual void Init(Action<ViewPresenterBase> closeAction)
        {
            SetCloseAction(closeAction);
            initViewAction = GetObjects;
        }

        private void SetCloseAction(Action<ViewPresenterBase> closeAction)
        {
            if (closeAction != null)
            {
                this.closeAction = closeAction;
            }
            else
            {
                this.closeAction = delegate(ViewPresenterBase panelPresenterBase) { SetViewVisible(false); };
            }
        }

        public void InitView(GameObject root)
        {
            _view= CreatViewInstance();
            initViewAction?.Invoke(root);
        }
        public virtual void GetObjects(GameObject root)
        {
            _view.Init(root);
            _view.GetObjects();
            OnInitView();
        }

        public virtual void OnInitView()
        {
        }

        public virtual void PreShow()
        {
        }

        public void Show(PanelDateBase panelDateBase)
        {
            SetViewVisible(true);
            this.date = panelDateBase;
            OnShow(panelDateBase);
        }

        protected virtual void OnShow(PanelDateBase panelDateBase)
        {
        }

        public void SetViewVisible(bool visible)
        {
            _view.SetViewVisible(visible);
        }

        protected virtual void PreClose()
        {
        }

        public virtual void Close()
        {
            PreClose();
            closeAction?.Invoke(this);
            AfterClose();
            this.date?.OnClose?.Invoke();
        }

        protected virtual void AfterClose()
        {
        }

        private bool Visible()
        {
            if (_view == null)
            {
                return false;
            }
            else
            {
                return _view.Visible();
            }
        }
        protected Coroutine StartCoroutine(IEnumerator enumerator)
        {
            return _view.StartCoroutine(enumerator);
        }
    }
}