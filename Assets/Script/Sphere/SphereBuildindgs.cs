using System.Collections.Generic;
using Assets.Script.Config;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereBuildindgs:SphereComponent
    {
        private List<BuildingInSphere> buildings;
        public SphereBuildindgs(Transform root) : base(root)
        {
            buildings = new List<BuildingInSphere>();
        }

        public void ShowBuilding(BuildingInSphere building)
        {
            building.Show();
            building.root.transform.SetParent(this.root);
            building.root.transform.position = SphereMap.GetPositionByGrid(building.grid);
            building.root.transform.localRotation=Quaternion.Euler(0,building.rotation,0);
            building.SetNavObstacle();
            buildings.Add(building);
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