using System.Collections.Generic;
using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereWall : SphereComponent
    {
        private Wall wall;

        public Wall AddWall(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                wall = new Wall(id);
                wall.onLoaded = OnShow;
            }
            return wall;
        }

        public void OnShow()
        {
            if (wall==null)
            {
                return;
            }
            wall.SetParent(this.root);
            wall.root.transform.localPosition = Vector3.zero;
            var levelConfig = SphereLevel.GetLevel(sphere.level.ToString());
            var scale = (float)levelConfig.scale;
            wall.root.transform.localScale = new Vector3(scale, scale, scale);
        }

        public SphereWall(Sphere sphere) : base(sphere)
        {
        }
    }
}