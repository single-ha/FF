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
        UIManager.Inst.OpenPanel<LoginPanelPresenter,LoginPanelView>();
    }

    void InitGame()
    {
        GameObject.DontDestroyOnLoad(this.transform.parent.gameObject);
        InitManagers();
    }
    void InitManagers()
    {
        ObjectPoolManager.Inst.Init();
        ResManager.Inst.Init();
        StageManager.Inst.Init();
        UIManager.Inst.Init();
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