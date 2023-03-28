using Assets.Script.Manager;
using LitJson;
using UnityEngine;

namespace Assets.Script.Config
{
    public abstract class StaticConfig<T>:ConfigReader where T:new()
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
        protected bool dirty = true;

        public bool Dirty
        {
            get { return dirty; }
        }

        private string config_name;

        public StaticConfig()
        {
            Init();
        }
        public void Init()
        {
            SetConfigName(ref config_name);
            LoadConfig();
        }

        public override void SetJson(JsonData json)
        {
            base.SetJson(json);
            SetDirty(true);
        }

        protected void SetDirty(bool dirty)
        {
            this.dirty = dirty;
        }

        private void LoadConfig()
        {
            var config = ResManager.Inst.Load<TextAsset>($"{config_name}.json");
            if (config == null)
            {
                Debuger.LogError($"配置文件不存在:{config_name}");
            }
            SetJson(JsonMapper.ToObject(config.text));
        }

        protected abstract void SetConfigName(ref string configName);
    }
}