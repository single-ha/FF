using Assets.Script.Config;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereEarth: SphereComponent
    {
        private int level;
        public int Level => level;
        private EarthConfig config;
        private GameObject obj;
        public SphereEarth(Transform root) : base(root)
        {
            
        }
        public void AddEarth(string id, int level)
        {
            this.level = level;
            config = EarthConfig.GetEarthSingleConfig(id);
            if (config != null)
            {
                if (string.IsNullOrEmpty(config.prefab))
                {
                    Debuger.LogError($"Earth({id}的prefab是空)");
                    return;
                }
                var o = ResManager.Inst.Load<GameObject>($"{config.prefab}.prefab");
                obj = GameObject.Instantiate(o,this.root);
                obj.transform.localPosition=Vector3.zero;
                obj.transform.localScale=Vector3.one;
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    var child = obj.transform.GetChild(i);
                    child.gameObject.SetActive(child.name==level.ToString());
                }
            }
            else
            {
                //earth中没有id为{id}的配置
                Debuger.LogError($"terrains中没有id为{id}的配置");
            }
        }
    }
}