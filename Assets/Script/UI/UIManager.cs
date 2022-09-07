using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Script.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using Enumerable = System.Linq.Enumerable;

public class UIManager
{
    private static UIManager _inst;

    public static UIManager Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = new UIManager();
            }

            return _inst;
        }
    }

    private GameObject root;
    public Camera uiCamera;
    private GameObject normalRoot;
    private GameObject popRoot;
    private GameObject systemRoot;

    private PanelPresenterBase curShow;
    private List<PanelPresenterBase> openPanels;
    private Dictionary<Type, List<PanelPresenterBase>> openPanelsDic;
    public IPanelChange panelChange;

    public void Init()
    {
        var obj = ResManager.Inst.Load<GameObject>("UIRoot.prefab");
        this.root = GameObject.Instantiate(obj);
        GameObject.DontDestroyOnLoad(this.root);
        if (root == null)
        {
            return;
        }

        uiCamera = root.transform.Find("Camera").GetComponent<Camera>();
        normalRoot = root.transform.Find("Canvas/NormalRoot").gameObject;
        popRoot = root.transform.Find("Canvas/PopRoot").gameObject;
        systemRoot = root.transform.Find("Canvas/SystemRoot").gameObject;

        openPanels = new List<PanelPresenterBase>();
        openPanelsDic = new Dictionary<Type, List<PanelPresenterBase>>();
    }

    public T OpenPanel<T,TW>(PanelDateBase panelDateBase = null) where T:PanelPresenterBase where TW:PanelBase,new()
    {
        T panel = Activator.CreateInstance<T>();
        panel.Init(ClosePanel);
        if (panel.panelConfig.unique && openPanelsDic.ContainsKey(panel.GetType()))
        {
            panel = (T)openPanelsDic[panel.GetType()].FirstOrDefault();
            ReOpenPanel(panel, panelDateBase);
            return panel;
        }
        panel.PreShow();
        if (panel.canOpen)
        {
            if (panelChange == null)
            {
                panelChange = NormalPanelChange.Inst;
            }
            GameObject patent = null;
            switch (panel.panelConfig.panelType)
            {
                case PanelType.Normal:
                    patent = normalRoot;
                    break;
                case PanelType.PoP:
                    patent = popRoot;
                    break;
                case PanelType.Sys:
                    patent = systemRoot;
                    break;
                default:
                    Debug.LogError("不存在的类型,使用默认节点normal");
                    patent = normalRoot;
                    break;
            }
            panel.InitPanel<TW>(patent);
            bool open = panelChange.Change(curShow, panel, panelDateBase);
            panelChange = null;
            if (open)
            {
                curShow = panel;
                openPanels.Add(panel);
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

                return panel;
            }

            return null;
        }
        else
        {
            panel = null;
            return null;
        }
    }

    public void ReOpenPanel(PanelPresenterBase panelPresenterBase, PanelDateBase panelDateBase = null)
    {
        int index = openPanels.IndexOf(panelPresenterBase);
        if (index >= 0)
        {
            int count = openPanels.Count;
            for (int i = count - 1; i > index; i--)
            {
                var panel = openPanels[i];
                panel.DestoryPanel();
                openPanels.RemoveAt(i);
                RemoveFormDic(panel);
            }

            openPanels[index].Show(panelDateBase);
        }
        else
        {
        }
    }

    private void RemoveFormDic(PanelPresenterBase panelPresenter)
    {
        openPanelsDic[panelPresenter.GetType()].Remove(panelPresenter);
        if (openPanelsDic[panelPresenter.GetType()].Count <= 0)
        {
            openPanelsDic.Remove(panelPresenter.GetType());
        }
    }

    public void ClosePanel( PanelPresenterBase panelPresenterBase)
    {
        int index = openPanels.IndexOf(panelPresenterBase);
        if (index >=0)
        {
            if (index - 1 >= 0&&index-1<openPanels.Count)
            {
                openPanels[index - 1].SetViewVisible(true);
            }
            openPanels[index].DestoryPanel();
            openPanels.RemoveAt(index);
            RemoveFormDic(panelPresenterBase);
        }
        else
        {
            Debug.Log($"no this panelPresenter:{panelPresenterBase.GetType()} or this panle is main panelPresenter");
            //当前打开的界面不包含在已打开界面列表中            
        }
    }

    /// <summary>
    /// 清除所有的界面
    /// </summary>
    public void ClearPanel()
    {
        for (int i = 0; i < openPanels.Count; i++)
        {
            openPanels[i].DestoryPanel();
        }

        openPanels.Clear();
        openPanelsDic.Clear();
    }
}