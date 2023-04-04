using System.Collections.Generic;
using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereEarth: SphereComponent
    {
        private Earth earth;
        public Earth AddEarth(string id)
        {
            earth = new Earth(id);
            earth.onLoaded = OnShow;
            return earth;
        }

        public void OnShow()
        {
            if (earth==null)
            {
                return;
            }
            earth.SetParent(this.root.transform);
            earth.root.transform.localPosition = Vector3.zero;
            var levelConfig = SphereLevel.GetLevel(sphere.level.ToString());
            var scale = (float)levelConfig.scale;
            earth.root.transform.localScale = new Vector3(scale, scale, scale);
        }

        public SphereEarth(Sphere sphere) : base(sphere)
        {
        }
    }
}