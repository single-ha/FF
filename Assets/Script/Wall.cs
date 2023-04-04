using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class Wall:StagePlayer
    {
        public Wall(string id):base(id)
        {
            this.id = id;
        }
        public override void Load()
        {
            var config = new WallConfig(id);
            if (string.IsNullOrEmpty(config.Prefab))
            {
                Debuger.LogError($"Earth({id}的prefab是空)");
                return;
            }
            var o = ResManager.Inst.Load<GameObject>($"{config.Prefab}.prefab");
            root = GameObject.Instantiate(o);
        }
    }
}