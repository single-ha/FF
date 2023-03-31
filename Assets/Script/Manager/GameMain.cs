using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Script.Manager;
using Assets.Script.UI;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public static GameMain Inst;
    public AssetLoadType assetType;
    public string assetRootPath;

    void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        InitGame();
        UIManager.Inst.OpenPanel<LoginPanelPresenter>();
    }

    void InitGame()
    {
        Application.targetFrameRate = 60;
        GameObject.DontDestroyOnLoad(this.transform.parent.gameObject);
        InitManagers();
    }
    void InitManagers()
    {
        for (int i = 0; i < ManagerList.Inst.managers.Count; i++)
        {
            ManagerList.Inst.managers[i].OnEnable();
        }
    }
    public void StopCoroutines(List<Coroutine> coroutines)
    {
        if (coroutines!=null)
        {
            for (int i = 0; i < coroutines.Count; i++)
            {
                StopCoroutine(coroutines[i]);
            }
        }
    }

    public void StartCoroutines(List<IEnumerator> enumerators)
    {
        if (enumerators != null)
        {
            for (int i = 0; i < enumerators.Count; i++)
            {
                StartCoroutine(enumerators[i]);
            }
        }
    }
}

public enum AssetLoadType
{
    Editor,
    Bundle,
}