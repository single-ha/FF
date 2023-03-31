using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereCover:SphereComponent
    {

        public SphereCover(Transform root) : base(root)
        {
        }
        public void AddCover(string id, int level)
        {
            var config = new CoverConfig(id);
            var levelConfig = SphereLevel.GetLevel(level.ToString());
            if (string.IsNullOrEmpty(config.Prefab))
            {
                Debuger.LogError($"cover({id}的prefab是空)");
                return;
            }
            var o = ResManager.Inst.Load<GameObject>($"{config.Prefab}.prefab");
            var obj = GameObject.Instantiate(o, this.root);
            obj.transform.localPosition = Vector3.zero;
            var scale = (float)levelConfig.scale;
            obj.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}