using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Script.Manager;
using LitJson;
using UnityEngine;

namespace Assets.Script.Data
{
    public class JsonReader
    {
        private JsonData json;

        public JsonReader(JsonData json)
        {
            this.json = json;
        }

        public JsonReader()
        {
        }

        public virtual void SetJson(JsonData json)
        {
            this.json = json;
        }
        public void ReadDictionary<TValue>(ref Dictionary<string, TValue> dic, string dic_key = null) where TValue : DataBase, new()
        {
            if (dic == null)
            {
                dic = new Dictionary<string, TValue>();
            }
            if (!this.json.IsObject)
            {
                return;
            }
            var json_dic = !string.IsNullOrEmpty(dic_key) && json.ContainsKey(dic_key) ? json[dic_key] : json;
            var j_keys = json_dic.Keys.ToList();
            var d_keys = dic.Keys.ToList();
            var del = d_keys.Except(j_keys).ToArray();
            for (int i = del.Length - 1; i >= 0; i--)
            {
                dic.Remove(del[i]);
            }

            for (int i = 0; i < j_keys.Count; i++)
            {
                var key = j_keys[i];
                if (dic.ContainsKey(key))
                {
                    dic[key].SetJson(json_dic[key]);
                }
                else
                {
                    TValue value = new TValue();
                    value.SetJson(json_dic[key]);
                    dic[key] = value;
                }
            }
        }

        public int ReadInt(string key, int value = 0)
        {
            if (json.ContainsKey(key))
            {
                return json[key].i;
            }
            else
            {
                return value;
            }
        }
        public string ReadStr(string key, string value = null)
        {
            if (json.ContainsKey(key))
            {
                return json[key].str;
            }
            else
            {
                return value;
            }
        }
        public bool ReadBool(string key, bool value = false)
        {
            if (json.ContainsKey(key))
            {
                return json[key].b;
            }
            else
            {
                return value;
            }
        }
        public double ReadDouble(string key, double value = 0)
        {
            if (json.ContainsKey(key))
            {
                return json[key].d;
            }
            else
            {
                return value;
            }
        }
        public void ReadArray(ref int[] arr, string key=null)
        {
            if (!json.ContainsKey(key))
            {
                Debuger.LogError($"配置中不包含key({key})");
                return;
            }
            JsonData t =string.IsNullOrEmpty(key)?json:json[key];
            if (arr==null||arr.Length!=t.Count)
            {
                arr = new int[t.Count];
            }

            for (int i = 0; i < t.Count; i++)
            {
                arr[i] = t[i].i;
            }
        }
        public void ReadArray(ref double[] arr, string key = null)
        {
            if (!json.ContainsKey(key))
            {
                Debuger.LogError($"配置中不包含key({key})");
                return;
            }
            JsonData t = string.IsNullOrEmpty(key) ? json : json[key];
            if (arr == null || arr.Length != t.Count)
            {
                arr = new double[t.Count];
            }

            for (int i = 0; i < t.Count; i++)
            {
                arr[i] = t[i].d;
            }
        }
        public void ReadArray(ref string[] arr, string key = null)
        {
            if (!json.ContainsKey(key))
            {
                Debuger.LogError($"配置中不包含key({key}):{json.ToJson()}");
                return;
            }
            JsonData t = string.IsNullOrEmpty(key) ? json : json[key];
            if (arr == null || arr.Length != t.Count)
            {
                arr = new string[t.Count];
            }

            for (int i = 0; i < t.Count; i++)
            {
                arr[i] = t[i].str;
            }
        }
        public void ReadArray(ref bool[] arr, string key = null)
        {
            if (!json.ContainsKey(key))
            {
                Debuger.LogError($"配置中不包含key({key})");
                return;
            }
            JsonData t = string.IsNullOrEmpty(key) ? json : json[key];
            if (arr == null || arr.Length != t.Count)
            {
                arr = new bool[t.Count];
            }

            for (int i = 0; i < t.Count; i++)
            {
                arr[i] = t[i].b;
            }
        }
    }
}