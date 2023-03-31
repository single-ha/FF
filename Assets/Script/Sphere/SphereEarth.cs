using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereEarth: SphereComponent
    {
        public SphereEarth(Transform root) : base(root)
        {
            
        }
        public void AddEarth(string id, int level)
        {
            var config = new EarthConfig(id);
            var levelConfig = SphereLevel.GetLevel(level.ToString());
            if (string.IsNullOrEmpty(config.prefab))
            {
                Debuger.LogError($"Earth({id}的prefab是空)");
                return;
            }
            var o = ResManager.Inst.Load<GameObject>($"{config.prefab}.prefab");
            var obj = GameObject.Instantiate(o, this.root);
            obj.transform.localPosition = Vector3.zero;
            var scale = (float)levelConfig.scale;
            obj.transform.localScale = new Vector3(scale, scale, scale);
            // for (int i = 0; i < obj.transform.childCount; i++)
            // {
            //     var child = obj.transform.GetChild(i);
            //     child.gameObject.SetActive(child.name == level.ToString());
            // }
        }
    }
}