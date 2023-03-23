using System.Collections.Generic;
using Assets.Script.Config;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereBuildindgs:SphereComponent
    {
        private Dictionary<string, GameObject> buildings;
        public SphereBuildindgs(Transform root) : base(root)
        {
            buildings = new Dictionary<string, GameObject>();
        }
        public void AddBuilding(string id, Vector2 grid)
        {
            AddBuilding(id,(int)grid.x, (int)grid.y);
        }
        public void AddBuilding(string id, int x,int y)
        {
            var buildingConfig = BuildingConfig.GetConfig(id);
            if (buildingConfig==null)
            {
                return;
            }
            string path = $"{buildingConfig.Prefab}.prefab";
            GameObject o = ResManager.Inst.Load<GameObject>(path);
            GameObject building = GameObject.Instantiate(o, this.root);
            building.transform.position = SphereMap.GetPositionByGrid(x, y);
            // buildings[$"{x}_{y}"] = building;
        }

        public void AddBuildings(List<BuildingInSphereData> sphereMapBuildings)
        {
            for (int i = 0; i < sphereMapBuildings.Count; i++)
            {
                var d = sphereMapBuildings[i];
                AddBuilding(d.id, d.grid);
            }
        }
    }
}