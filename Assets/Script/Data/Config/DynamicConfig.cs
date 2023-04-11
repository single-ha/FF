using Assets.Script.Manager;
using LitJson;
using UnityEngine;

namespace Assets.Script.Data
{
    public class DynamicConfig:DataBase
    {
        public DynamicConfig(string configName)
        {
            GetJsonData(configName);
        }

        public DynamicConfig()
        {
        }

        public virtual void GetJsonData(string configName)
        {
            var config = ResManager.Inst.Load<TextAsset>($"{configName}.json");
            if (config == null)
            {
                Debuger.LogError($"配置文件不存在:{configName}");
            }
            else
            {
                SetJson(JsonMapper.ToObject(config.text));
            }
        }
    }
}