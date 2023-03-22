using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Script
{
    public abstract class SphereComponent
    {
        protected Transform root;

        public SphereComponent(Transform root)
        {
            this.root = root;
        }
    }
}