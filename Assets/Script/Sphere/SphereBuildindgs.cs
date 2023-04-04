using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script
{
    public class SphereBuildindgs:SphereComponent
    {
        private List<BuildingInSphere> buildingsList;
        private Dictionary<GameObject,BuildingInSphere> buildings;
        public SphereBuildindgs(Sphere sphere) : base(sphere)
        {
            buildingsList = new List<BuildingInSphere>();
            buildings = new Dictionary<GameObject, BuildingInSphere>();
        }

        public void ShowBuilding(BuildingInSphere building)
        {
            building.onLoaded = delegate()
            {
                OnShow(building);
            };
            buildingsList.Add(building);

        }
        public void ShowBuildings(List<BuildingInSphere> sphereMapBuildings)
        {
            for (int i = 0; i < sphereMapBuildings.Count; i++)
            {
                var d = sphereMapBuildings[i];
                ShowBuilding(d);
            }
        }

        public BuildingInSphere GetBuilding(GameObject gameObject)
        {
            if (buildings.ContainsKey(gameObject))
            {
                return buildings[gameObject];
            }
            else
            {
                return null;
            }
        }

        public void OnShow(BuildingInSphere building)
        {
            building.SetParent(this.root.transform);
            building.root.transform.position = SphereMap.GetPositionByGrid(building.grid);
            building.root.transform.localRotation = Quaternion.Euler(0, building.rotation, 0);
            building.SetNavObstacleAndCollider();
            building.SetLayer("Building");
            buildings[building.root] = building;
        }
    }
}