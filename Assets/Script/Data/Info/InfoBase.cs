using System.IO;
using Assets.Script.Manager;
using LitJson;
using UnityEngine;

namespace Assets.Script.Data
{
    public abstract class InfoBase<T>:DataBase where T : new()
    {
        private static T _inst;

        public static T Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new T();
                }

                return _inst;
            }
        }

        protected InfoBase()
        {
            var name = GetJsonName();
            var j = GetJsonData(name);
            SetJson(j);
        }

        protected abstract string GetJsonName();

        public bool dirty = true;
        protected void SetDirty(bool dirty)
        {
            this.dirty = dirty;
        }
        public override void SetJson(JsonData json)
        {
            base.SetJson(json);
            SetDirty(true);
        }

        public virtual JsonData GetJsonData(string name)
        {
            var path = Path.Combine(Application.persistentDataPath, "info", "spheres");
            string text = "";
            if (File.Exists(path))
            {
                text = File.ReadAllText(path);
            }
            else
            {
                var asset = ResManager.Inst.Load<TextAsset>($"{name}.json");
                if (asset!=null)
                {
                    text = asset.text;
                }
            }

            if (string.IsNullOrEmpty(text))
            {
                return new JsonData();
            }
            else
            {
                return JsonMapper.ToObject(text);
            }
        }
    }
}