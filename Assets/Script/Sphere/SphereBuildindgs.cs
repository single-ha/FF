using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class SphereBuildindgs:SphereComponent
    {
        private Dictionary<GameObject,BuildingInSphere> buildings;
        public SphereBuildindgs(Transform root) : base(root)
        {
            buildings = new Dictionary<GameObject, BuildingInSphere>();
        }

        public void ShowBuilding(BuildingInSphere building)
        {
            building.Show();
            building.root.transform.SetParent(this.root);
            building.root.transform.position = SphereMap.GetPositionByGrid(building.grid);
            building.root.transform.localRotation=Quaternion.Euler(0,building.rotation,0);
            building.SetNavObstacleAndCollider();
            building.SetLayer("Building");
            buildings[building.root]=building;
        }
        public void ShowBuildings(List<BuildingInSphere> sphereMapBuildings)
        {
            for (int i = 0; i < sphereMapBuildings.Count; i++)
            {
                var d = sphereMapBuildings[i];
                ShowBuilding(d);
            }
        }
    }
}