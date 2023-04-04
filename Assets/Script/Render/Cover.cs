using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class Cover:StagePlayer
    {

        public Cover(string id):base(id)
        {
            this.id = id;
        }
        public override void Load()
        {
            var config = new CoverConfig(id);
            if (string.IsNullOrEmpty(config.Prefab))
            {
                Debuger.LogError($"Cover({id}的prefab是空)");
                return;
            }
            var o = ResManager.Inst.Load<GameObject>($"{config.Prefab}.prefab");
            root = GameObject.Instantiate(o);
        }
    }
}