﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Script.Manager
{
    public class ResManager : IManager
    {
        private static ResManager _inst;

        public static ResManager Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new ResManager();
                }

                return _inst;
            }
        }

        private ResManager()
        {

        }

        private Dictionary<string, List<Coroutine>> loadList;

        private IResLoader loader;

        public void OnEnable()
        {
            loadList = new Dictionary<string, List<Coroutine>>();
            string path = null;
#if UNITY_EDITOR
            switch (GameMain.Inst.assetType)
            {
                case AssetLoadType.Editor:
                    loader = new Loder_Editor();
                    path = Application.dataPath;
                    break;
                case AssetLoadType.Bundle:
                    loader = new Loader_Bundle();
                    path = GameMain.Inst.assetRootPath;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
#else
            loader = new Loader_Bundle();
            path = Application.persistentDataPath;
#endif
            loader.Init(path);
        }

        public T Load<T>(string assetName) where T : Object
        {
            if (loader == null)
            {
                OnEnable();
            }

            return loader.LoadAsset<T>(assetName);
        }

        public void LoadAsync<T>(string assetName, Action<T> callBack) where T : Object
        {
            if (loader == null)
            {
                OnEnable();
            }

            var cor = GameMain.Inst.StartCoroutine(loader.LoadAssetAsync<T>(assetName, delegate (T t)
            {
                if (loadList.ContainsKey(assetName))
                {
                    loadList.Remove(assetName);
                }

                callBack.Invoke(t);
            }));
            if (loadList.ContainsKey(assetName))
            {
                loadList[assetName].Add(cor);
            }
            else
            {
                List<Coroutine> l = new List<Coroutine>();
                l.Add(cor);
                loadList[assetName] = l;
            }
        }

        public void StopAllLoad()
        {
            if (loader is IResLoader_Stop)
            {
                ((IResLoader_Stop)loader).StopAllLoad();
            }
            var temp = loadList.Values.ToArray();
            for (int i = 0; i < temp.Length; i++)
            {
                GameMain.Inst.StopCoroutines(temp[i]);
            }
        }

        public void StopLoad(string assetName)
        {
            if (loader is IResLoader_Stop)
            {
                ((IResLoader_Stop)loader).StopLoad(assetName);
                if (loadList.ContainsKey(assetName))
                {
                    var list = loadList[assetName];
                    for (int i = 0; i < list.Count; i++)
                    {
                        GameMain.Inst.StopCoroutine(list[i]);
                    }
                }

                loadList.Remove(assetName);
            }
        }

        void OnGUI()
        {
            if (loader is Loader_Bundle)
            {
                GUIStyle style = new GUIStyle();
                style.fontSize = 20;
                GUILayout.Label($"Bundle Num:{((Loader_Bundle)loader).abMap.Count}", style);
            }
        }
    }
}
