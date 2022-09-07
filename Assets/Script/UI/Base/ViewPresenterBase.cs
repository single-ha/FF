using System;
using UnityEngine;

namespace Assets.Script.UI
{
    public abstract class ViewPresenterBase
    {
        protected ViewBase _view;
        protected PanelDateBase date;

        protected T View<T>() where T:ViewBase,new()
        {
            if (_view==null)
            {
                _view = new T();
            }
            return (T)_view;
        }

        public virtual void Init()
        {

        }
        public virtual void PreShow()
        {

        }
        public void InitView(GameObject root)
        {
            _view.Init(root);
            OnInitView();
        }
        public virtual void OnInitView()
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
            SetViewVisible(false);
            AfterClose();
            this.date?.OnClose?.Invoke();
        }
        protected virtual void AfterClose()
        {

        }
    }
}