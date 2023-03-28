using Assets.Script.Config;
using Assets.Script.Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script
{
    public class SphereTerrain : SphereComponent
    {
        private string defaultTerrain = "terrain_{0}";
        private int level;
        public int Level => level;

        private TerrainConfig config;
        private GameObject _terrainObj;

        public GameObject TerrainObj
        {
            get => _terrainObj;
        }


        public SphereTerrain(Transform root) : base(root)
        {
        }

        public void AddTerrain(string id, int level)
        {
            this.level = level;
            config = TerrainConfig.GetTerrainConfig(id);
            if (config != null)
            {
                if (!string.IsNullOrEmpty(config.prefab_name))
                {
                    defaultTerrain = config.prefab_name;
                }
                var o = ResManager.Inst.Load<GameObject>($"{string.Format(defaultTerrain, this.level)}.prefab");
                _terrainObj = GameObject.Instantiate(o);
                var material = ResManager.Inst.Load<Material>($"{config.material}.mat");
                AddTerrainMaterial(material);
                _terrainObj.SetLayer(LayerMask.NameToLayer("Terrain"));
                //烘培
                BackNavMesh();
            }
            else
            {
                Debuger.LogError($"terrains中没有id为{id}的配置");
            }
        }
        /// <summary>
        /// 烘培导航网格
        /// </summary>
        public void BackNavMesh()
        {
            NavMeshSurface navMesh = _terrainObj.GetComponentInChildren<NavMeshSurface>();
            navMesh.RemoveData();
            navMesh.BuildNavMesh();
        }
        private void AddTerrainMaterial(Material m)
        {
            Tool.ClearChild(this.root);
            if (_terrainObj == null)
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