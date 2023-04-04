using System.Collections.Generic;
using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script
{
    public class SphereTerrain : SphereComponent
    {
        private Terrain terrain;
        public Terrain AddTerrain(string id, int level)
        {
            if (!string.IsNullOrEmpty(id))
            {
                terrain = new Terrain(id, level);
                terrain.onLoaded = OnShow;
            }
            return terrain;
        }

        public SphereTerrain(Sphere sphere) : base(sphere)
        {
        }
        public void OnShow()
        {
            if (terrain==null)
            {
                return;
            }
            terrain.SetParent(this.root.transform);
            terrain.root.transform.localPosition = Vector3.zero;
        }
    }
}