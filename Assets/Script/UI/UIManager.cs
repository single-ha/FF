using System;
using System.Collections.Generic;
using Assets.Script.UI.Base;
using UnityEngine;
using UnityEngine.Rendering;

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

    private PanelBase curShow;
    private List<PanelBase> openPanels;

    public IPanelChange panelChange;

    public void Init()
    {
        this.root = GameObject.Find("UIRoot");
        if (root == null)
        {
            return;
        }

        uiCamera = root.transform.Find("Camera").GetComponent<Camera>();
        normalRoot = root.transform.Find("Canvas/NormalRoot").gameObject;
        popRoot = root.transform.Find("Canvas/PopRoot").gameObject;
        systemRoot = root.transform.Find("Canvas/SystemRoot").gameObject;

        openPanels = new List<PanelBase>();
    }

    public PanelBase OpenPanel<T>(UIDate uiDate=null) where T : PanelBase, new()
    {
        T panel = Activator.CreateInstance<T>();
        panel.PreShow();
        if (panel.canOpen)
        {
            if (panelChange == null)
            {
                panelChange = NormalPanelChange.Inst;
            }
            string name = $"{panel.panelConfig.name}.prefab";
            var o = ResManager.Inst.Load<GameObject>(name);
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
            var obj = GameObject.Instantiate(o, patent.transform);
            panel.Init(obj);
            bool open = panelChange.Change(curShow, panel, uiDate);
            if (open)
            {
                curShow = panel;
                if (panel.panelConfig.index < 0)
                {
                    openPanels.Add(panel);
                }
                else
                {
                    openPanels.Insert(panel.panelConfig.index, panel);
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

    public void ReturnPanel(PanelBase panelBase,params object[] datas)
    {
        int index = openPanels.IndexOf(panelBase);
        if (index>=0)
        {
            int count = openPanels.Count;
            for (int i = count-1; i >index; i--)
            {
                var panel= openPanels[i];
                panel.Destory();
                openPanels.RemoveAt(i);
            }
            openPanels[index].ReturnPanel(datas);
        }
        else
        {
            
        }
    }
    public void ReturnPanel(string panelName, params object[] datas)
    {
        for (int i = openPanels.Count-1; i >=0 ; i--)
        {
            var panel = openPanels[i];
            if (panel.panelConfig.name==panelName)
            {
                ReturnPanel(panel, datas);
                break;
            }
        }
    }
    public void ClosePanle(PanelBase panelBase)
    {
        int index = openPanels.IndexOf(panelBase);
        if (index > 0)
        {
            openPanels[index].Destory();
            openPanels[index - 1].SetViewVisible(true);
        }
        else
        {
            Debug.Log($"no this panel:{panelBase.panelConfig.name} or this panle is main panel");
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
            openPanels[i].Destory();
        }
        openPanels.Clear();
    }
}