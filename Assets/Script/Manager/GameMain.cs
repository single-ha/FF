using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        UIManager.Inst.Init();
        UIManager.Inst.OpenPanel<LoginPanel>();
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
}

public enum AssetLoadType
{
    Editor,
    Bundle,
}