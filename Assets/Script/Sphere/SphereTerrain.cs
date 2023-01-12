using Assets.Script.Config;
using UnityEngine;

namespace Assets.Script
{
    public class SphereTerrain:SphereComponent
    {
        private string defaultTerrain = "terrain_{0}";
        private Terrains_Config.Terrain_Config config;
        private GameObject _terrainObj;

        public Terrains_Config.Terrain_Config Config
        {
            get => config;
        }

        public GameObject TerrainObj
        {
            get => _terrainObj;
        }

        private SphereTerrainMask _sphereTerrainMask;
        public SphereTerrain(Transform root) : base(root)
        {
            _sphereTerrainMask = new SphereTerrainMask(this);
        }

        public override void SetComponent(string id, int level)
        {
            var terrains = Terrains_Config.Inst.Terrains;
            if (terrains.ContainsKey(id))
            {
                config = terrains[id];
                if (!string.IsNullOrEmpty(config.prefab_name))
                {
                    defaultTerrain = config.prefab_name;
                }
                var o = ResManager.Inst.Load<GameObject>($"{string.Format(defaultTerrain, level)}.prefab");
                _terrainObj = GameObject.Instantiate(o);
                var material = ResManager.Inst.Load<Material>($"{config.material}.mat");
                SetComponent(material);
                // _sphereTerrainMask.ShowMask();
            }
            else
            {
                Debuger.LogError($"terrains中没有id为{id}的配置");
            }
        }
        public void SetComponent(Material m)
        {
            Tool.ClearChild(this.root);
            if (_terrainObj==null)
            {
                return;
            }
            _terrainObj.transform.SetParent(this.root);
            var meshs = _terrainObj.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshs.Length; i++)
            {
                meshs[i].sharedMaterial = m;
            }
        }


    }
}