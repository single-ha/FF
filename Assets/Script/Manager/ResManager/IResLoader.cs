using System;
using System.Collections;
using Object = UnityEngine.Object;

public interface IResLoader
{
    void Init(string path);
    T LoadAsset<T>(string assetName) where T : Object;
    IEnumerator LoadAssetAsync<T>(string assetname, Action<T> callBack) where T : Object;
}

public interface IResLoader_Stop
{
    void StopAllLoad();
    void StopLoad(string assetName);
}