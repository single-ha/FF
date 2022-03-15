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
}

public enum AssetLoadType
{
    Editor,
    Bundle,
}