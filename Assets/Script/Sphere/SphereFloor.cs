using Assets.Script.Config;
using UnityEngine;

namespace Assets.Script
{
    public class SphereFloor:SphereComponent
    {
        private string defaultFloor = "floor_{0}";
        private Floors_Config.Floor_config config;
        private GameObject floorObj;

        public Floors_Config.Floor_config Config
        {
            get => config;
        }

        public GameObject FloorObj
        {
            get => floorObj;
        }

        private FloorHelp floorHelp;
        public SphereFloor(Transform root) : base(root)
        {
            floorHelp = new FloorHelp(this);
        }

        public override void SetComponent(string id, int level)
        {
            var floors = Floors_Config.Inst.Floors;
            if (floors.ContainsKey(id))
            {
                config = floors[id];
                if (!string.IsNullOrEmpty(config.prefab_name))
                {
                    defaultFloor = config.prefab_name;
                }
                var o = ResManager.Inst.Load<GameObject>($"{string.Format(defaultFloor, level)}.prefab");
                floorObj = GameObject.Instantiate(o);
                var material = ResManager.Inst.Load<Material>($"{config.material}.mat");
                SetComponent(material);
                floorHelp.ShowMask();
            }
            else
            {
                Debuger.LogError($"floors中没有id为{id}的配置");
            }
        }
        public void SetComponent(Material m)
        {
            Tool.ClearChild(this.root);
            if (floorObj==null)
            {
                return;
            }
            floorObj.transform.SetParent(this.root);
            var meshs = floorObj.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshs.Length; i++)
            {
                meshs[i].sharedMaterial = m;
            }
        }


    }
}