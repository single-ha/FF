using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script
{
    public class Terrain:StagePlayer
    {
        private const string defaultTerrain = "terrain_{0}";
        private int level;

        public Terrain(string id, int level):base(id)
        {
            this.id = id;
            this.level = level;
        }
        public override void Load()
        {
            var config = new TerrainConfig(id);
            var prefab = defaultTerrain;
            if (!string.IsNullOrEmpty(config.Prefab))
            {
                prefab = config.Prefab;
            }
            var o = ResManager.Inst.Load<GameObject>($"{string.Format(prefab, this.level)}.prefab");
            root = GameObject.Instantiate(o);
            var material = ResManager.Inst.Load<Material>($"{config.material}.mat");
            AddTerrainMaterial(material);
            root.SetLayer(LayerMask.NameToLayer("Terrain"));
            //烘培
            BackNavMesh();
        }
        private void AddTerrainMaterial(Material m)
        {
            if (root == null)
            {
                return;
            }
            var meshs = root.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshs.Length; i++)
            {
                meshs[i].sharedMaterial = m;
            }
        }
        /// <summary>
        /// 烘培导航网格
        /// </summary>
        public void BackNavMesh()
        {
            NavMeshSurface navMesh = root.GetComponentInChildren<NavMeshSurface>();
            navMesh.RemoveData();
            navMesh.BuildNavMesh();
        }
    }
}