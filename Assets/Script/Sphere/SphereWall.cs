using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereWall:SphereComponent
    {
        public SphereWall(Transform root) : base(root)
        {
        }

        public void AddWall(string id, int level)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            var config = new WallConfig(id);
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