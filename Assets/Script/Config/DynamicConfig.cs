using Assets.Script.Manager;
using LitJson;
using UnityEngine;

namespace Assets.Script.Config
{
    public class DynamicConfig:ConfigReader
    {
        public DynamicConfig(string configName)
        {
            var config = ResManager.Inst.Load<TextAsset>($"{configName}.json");
            if (config == null)
            {
                Debuger.LogError($"配置文件不存在:{configName}");
            }
            SetJson(JsonMapper.ToObject(config.text));
        }
    }
}