using System.Collections.Generic;
using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereRoof: SphereComponent
    {
        private Roof roof;

        public Roof AddRoof(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                roof = new Roof(id);
                roof.onLoaded = OnShow;
            }
            return roof;
        }

        public SphereRoof(Sphere sphere) : base(sphere)
        {
        }

        public void OnShow()
        {
            if (roof==null)
            {
                return;
            }
            roof.SetParent(this.root.transform);
            roof.root.transform.localPosition = Vector3.zero;
            var levelConfig = SphereLevel.GetLevel(sphere.level.ToString());
            var scale = (float)levelConfig.roof_scale;
            roof.root.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}