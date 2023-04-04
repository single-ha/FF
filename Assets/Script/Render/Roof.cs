using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class Roof:StagePlayer
    {
        public Roof(string id) : base(id)
        {
        }

        public override void Load()
        {
            var config = new RoofConfig(id);
            if (string.IsNullOrEmpty(config.Prefab))
            {
                Debuger.LogError($"roof({id}的prefab是空)");
                return;
            }
            var o = ResManager.Inst.Load<GameObject>($"{config.Prefab}.prefab");
            root = GameObject.Instantiate(o);
        }
    }
}