using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereRing: SphereComponent
    {
        public SphereRing(Transform root) : base(root)
        {

        }

        public void AddRing(string id, int level)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            var config = new RingConfig(id);
            var levelConfig = SphereLevel.GetLevel(level.ToString());
            if (string.IsNullOrEmpty(config.prefab))
            {
                Debuger.LogError($"Ring({id}的prefab是空)");
                return;
            }
            var o = ResManager.Inst.Load<GameObject>($"{config.prefab}.prefab");
            var obj = GameObject.Instantiate(o, this.root);
            obj.transform.localPosition = Vector3.zero;
            var scale = (float)levelConfig.scale;
            obj.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}