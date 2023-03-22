using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Script.UI;
using UnityEngine;

namespace Assets.Script.Manager
{
    public class UIManager :Instance<UIManager>, IManager
    {
        private GameObject root;
        public Camera uiCamera;
        private GameObject normalRoot;
        private GameObject popRoot;

        private GameObject systemRoot;

        //normal
        private List<PanelPresenterBase> normalPanels;

        //pop
        private List<PanelPresenterBase> popPanels;

        //normal panel 持有的pop
        private Dictionary<Type, List<PanelPresenterBase>> popHandle;

        //sys
        private List<PanelPresenterBase> sys_Panels;
        private Dictionary<Type, List<PanelPresenterBase>> openPanelsDic;
        public IOpenPanel openPanel;
        public IPanelClose closePanel;

        public void OnEnable()
        {
            if (this.root == null)
            {
                var obj = ResManager.Inst.Load<GameObject>("UIRoot.prefab");
                this.root = GameObject.Instantiate(obj);
                GameObject.DontDestroyOnLoad(this.root);
            }

            if (root == null)
            {
                return;
            }
            uiCamera = root.transform.Find("UICamera").GetComponent<Camera>();
            normalRoot = root.transform.Find("Canvas/NormalRoot").gameObject;
            popRoot = root.transform.Find("Canvas/PopRoot").gameObject;
            systemRoot = root.transform.Find("Canvas/SystemRoot").gameObject;
            normalPanels = new List<PanelPresenterBase>();
            popPanels = new List<PanelPresenterBase>();
            sys_Panels = new List<PanelPresenterBase>();
            openPanelsDic = new Dictionary<Type, List<PanelPresenterBase>>();
            popHandle = new Dictionary<Type, List<PanelPresenterBase>>();
            StageManager.Inst.AddOverLayCamera(uiCamera);
        }


        public T OpenPanel<T>(PanelDateBase panelDateBase = null) where T : PanelPresenterBase
        {
            T panel = Activator.CreateInstance<T>();
            panel.PreShow();
            if (!panel.canOpen)
            {
                return null;
            }
            panel.Init(ClosePanel);
            if (panel.PanelConfig.unique && openPanelsDic.ContainsKey(panel.GetType()))
            {
                panel = (T)openPanelsDic[panel.GetType()].FirstOrDefault();
                ReOpenPanel(panel, panelDateBase);
                return panel;
            }

            if (openPanel == null)
            {
                openPanel = GetDefaultOpen(panel.PanelConfig.panelType);
            }

            GameObject patent = null;
            switch (panel.PanelConfig.panelType)
            {
                case PanelType.Normal:
                case PanelType.PoP:
                    patent = normalRoot;
                    break;
                case PanelType.Sys:
                    patent = systemRoot;
                    break;
                default:
                    Debug.LogError("不存在的类型,使用默认节点normal");
                    patent = normalRoot;
                    break;
            }

            panel.InitView(patent);
            bool open = openPanel.Open(panel, panelDateBase);
            openPanel = null;
            if (open)
            {
                return panel;
            }

            return null;
        }

        public void ReOpenPanel(PanelPresenterBase panelPresenterBase, PanelDateBase panelDateBase = null)
        {
            List<PanelPresenterBase> list = GetPanelList(panelPresenterBase.PanelConfig.panelType);
            int index = list.IndexOf(panelPresenterBase);
            if (index >= 0)
            {
                list.RemoveAt(index);
                list.Add(panelPresenterBase);
                panelPresenterBase.Show(panelDateBase);
            }
            else
            {
            }
        }

        public void SetNormalVisible(PanelPresenterBase panel, bool visible)
        {
            panel.SetViewVisible(visible);
            Type t = panel.GetType();
            if (popHandle.ContainsKey(t))
            {
                for (int i = 0; i < popHandle[t].Count; i++)
                {
                    popHandle[t][i].SetViewVisible(visible);
                }
            }
        }

        public int GetNormalIndex(PanelPresenterBase panel)
        {
            return normalPanels.IndexOf(panel);
        }

        public PanelPresenterBase GetTopNormal()
        {
            return GetNormalPanel(normalPanels.Count - 1);
        }

        public PanelPresenterBase GetNormalPanel(int index)
        {
            if (normalPanels.Count > index && index >= 0)
            {
                return normalPanels[index];
            }

            return null;
        }

        private void Add2Dic(PanelPresenterBase panel)
        {
            if (openPanelsDic.ContainsKey(panel.GetType()))
            {
                openPanelsDic[panel.GetType()].Add(panel);
            }
            else
            {
                List<PanelPresenterBase> temp = new List<PanelPresenterBase>();
                temp.Add(panel);
                openPanelsDic[panel.GetType()] = temp;
            }
        }

        public void AddNormal(PanelPresenterBase panel)
        {
            normalPanels.Add(panel);
            Add2Dic(panel);
        }

        public void AddPop(PanelPresenterBase pop, PanelPresenterBase handel = null)
        {
            popPanels.Add(pop);
            if (handel == null)
            {
                handel = GetTopNormal();
            }

            AddPop2Handel(pop, handel);
            Add2Dic(pop);
        }

        private void AddPop2Handel(PanelPresenterBase pop, PanelPresenterBase handel)
        {
            if (handel == null)
            {
                return;
            }

            Type t = handel.GetType();
            if (popHandle.ContainsKey(t))
            {
                popHandle[t].Add(pop);
            }
            else
            {
                List<PanelPresenterBase> temp = new List<PanelPresenterBase>();
                temp.Add(pop);
                popHandle[t] = temp;
            }
        }

        public void AddSys(PanelPresenterBase panel)
        {
            sys_Panels.Add(panel);
            Add2Dic(panel);
        }

        private void RemoveFormDic(PanelPresenterBase panelPresenter)
        {
            Type t = panelPresenter.GetType();
            if (openPanelsDic.ContainsKey(t) && openPanelsDic[t].Contains(panelPresenter))
            {
                openPanelsDic[t].Remove(panelPresenter);
                if (openPanelsDic[t].Count <= 0)
                {
                    openPanelsDic.Remove(t);
                }
            }
        }

        public void RemoveNormal(PanelPresenterBase normalPanel)
        {
            if (normalPanels.Contains(normalPanel))
            {
                normalPanels.Remove(normalPanel);
            }

            RemovePopByNormal(normalPanel);
            RemoveFormDic(normalPanel);
        }

        /// <summary>
        /// 把pop界面从列表中移除
        /// </summary>
        /// <param name="popPanel"></param>
        public void RemovePop(PanelPresenterBase popPanel)
        {
            if (popPanels.Contains(popPanel))
            {
                popPanels.Remove(popPanel);
            }

            List<Type> needRemove = new List<Type>();
            foreach (var handle in popHandle)
            {
                if (handle.Value.Contains(popPanel))
                {
                    handle.Value.Remove(popPanel);
                    RemoveFormDic(popPanel);
                }

                if (handle.Value.Count <= 0)
                {
                    needRemove.Add(handle.Key);
                }
            }

            if (needRemove.Count >= 0)
            {
                for (int i = 0; i < needRemove.Count; i++)
                {
                    popHandle.Remove(needRemove[i]);
                }
            }
        }

        public void RemovePop(PanelPresenterBase handle, PanelPresenterBase popPanel)
        {
            if (popPanels.Contains(popPanel))
            {
                popPanels.Remove(popPanel);
            }

            Type t = handle.GetType();
            if (popHandle.ContainsKey(t))
            {
                var pops = popHandle[t];
                if (pops.Contains(popPanel))
                {
                    pops.Remove(popPanel);
                }

                if (pops.Count <= 0)
                {
                    popHandle.Remove(t);
                }
            }

            RemoveFormDic(popPanel);
        }

        public void RemovePopByNormal(PanelPresenterBase normalPanel)
        {
            Type t = normalPanel.GetType();
            if (popHandle.ContainsKey(t))
            {
                for (int i = 0; i < popHandle[t].Count; i++)
                {
                    RemovePop(normalPanel, popHandle[t][i]);
                }
            }
        }

        public void RemoveSys(PanelPresenterBase panel)
        {
            if (sys_Panels.Contains(panel))
            {
                sys_Panels.Remove(panel);
            }
        }

        public void ClosePanel(ViewPresenterBase presenterBase)
        {
            if (!(presenterBase is PanelPresenterBase))
            {
                return;
            }

            var panelPresenter = presenterBase as PanelPresenterBase;
            if (closePanel == null)
            {
                closePanel = GetDefaultClose(panelPresenter.PanelConfig.panelType);
            }

            closePanel.Close(panelPresenter);
            closePanel = null;
        }

        public void ClosePoPByHandle(PanelPresenterBase handlePanel)
        {
            Type t = handlePanel.GetType();
            if (popHandle.ContainsKey(t))
            {
                var pops = popHandle[t];
                for (int i = 0; i < pops.Count; i++)
                {
                    var pop = pops[i];
                    pop.DestoryPanel();
                }

                RemovePopByNormal(handlePanel);
            }
        }

        private IOpenPanel GetDefaultOpen(PanelType panelType)
        {
            switch (panelType)
            {
                case PanelType.Normal:
                    return OpenPanel_Normal.Inst;
                case PanelType.PoP:
                    return OpenPanel_Pop.Inst;
                case PanelType.Sys:
                    return OpenPanel_Pop.Inst;
                default:
                    return OpenPanel_Pop.Inst;
            }
        }

        private IPanelClose GetDefaultClose(PanelType panelType)
        {
            switch (panelType)
            {
                case PanelType.Normal:
                    return ClosePanel_Normal.Inst;
                case PanelType.PoP:
                    return ClosePanel_Pop.Inst;
                case PanelType.Sys:
                    return ClosePanel_Sys.Inst;
                default:
                    Debug.LogError("不支持的界面类型");
                    return ClosePanel_Pop.Inst;
            }
        }

        private List<PanelPresenterBase> GetPanelList(PanelType panelType)
        {
            switch (panelType)
            {
                case PanelType.Normal:
                    return normalPanels;
                case PanelType.PoP:
                    return popPanels;
                case PanelType.Sys:
                    return sys_Panels;
                default:
                    return normalPanels;
            }
        }

        /// <summary>
        /// 清除所有的界面
        /// </summary>
        public void ClearPanel()
        {
            for (int i = 0; i < normalPanels.Count; i++)
            {
                normalPanels[i].Close();
            }

            for (int i = 0; i < sys_Panels.Count; i++)
            {
                sys_Panels[i].Close();
            }

            normalPanels.Clear();
            sys_Panels.Clear();
            openPanelsDic.Clear();
        }
    }
}