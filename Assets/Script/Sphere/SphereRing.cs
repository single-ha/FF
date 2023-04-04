using System.Collections.Generic;
using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereRing: SphereComponent
    {
        private Ring ring;

        public Ring AddRing(string id, int level)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ring = new Ring(id);
                ring.onLoaded = Show;
            }
            return ring;
        }

        public SphereRing(Sphere sphere) : base(sphere)
        {
        }


        public void Show()
        {
            if (ring==null)
            {
                return;
            }
            ring.SetParent(this.root.transform);
            ring.root.transform.localPosition = Vector3.zero;
            var levelConfig = SphereLevel.GetLevel(sphere.level.ToString());
            var scale = (float)levelConfig.scale;
            ring.root.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}