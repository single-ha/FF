using Assets.Script.Manager;
using LitJson;
using UnityEngine;

namespace Assets.Script.Data
{
    public abstract class StaticConfig<T> where T:new()
    {
        private static T _inst;
        protected JsonReader reader;
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
        protected bool dirty = true;

        public bool Dirty
        {
            get { return dirty; }
        }

        protected string configName;

        public StaticConfig()
        {
            ConfigName();
            var j = GetJsonData();
            SetJson(j);
        }
        public void SetJson(JsonData json)
        {
            if (reader==null)
            {
                reader = new JsonReader();
            }
            reader.SetJson(json);
            SetDirty(true);
        }
        protected abstract void ConfigName();
        protected void SetDirty(bool dirty)
        {
            this.dirty = dirty;
        }

        public virtual JsonData GetJsonData()
        {
            var config = ResManager.Inst.Load<TextAsset>($"{configName}.json");
            if (config == null)
            {
                Debuger.LogError($"配置文件不存在:{configName}");
                return new JsonData();
            }
            else
            {
                return JsonMapper.ToObject(config.text);
            }

        }
    }
}