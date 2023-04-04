using System.Collections.Generic;
using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereCover:SphereComponent
    {
        private Cover cover;
     
        public Cover AddCover(string id, int level)
        {
            if (!string.IsNullOrEmpty(id))
            {
                cover = new Cover(id);
                cover.onLoaded = OnShow;
            }
            return cover;
        }

        public SphereCover(Sphere sphere) : base(sphere)
        {
        }

        public void OnShow()
        {
            if (cover==null)
            {
                return;
            }
            cover.SetParent(this.root.transform);
            cover.root.transform.localPosition = Vector3.zero;
            var levelConfig = SphereLevel.GetLevel(sphere.level.ToString());
            var scale = (float)levelConfig.scale;
            cover.root.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}