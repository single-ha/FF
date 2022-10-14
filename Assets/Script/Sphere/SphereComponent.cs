using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Script
{
    public abstract class SphereComponent
    {
        public Transform root;

        public SphereComponent(Transform root)
        {
            this.root = root;
        }
        public abstract void SetComponent(string id, int level);
    }
}