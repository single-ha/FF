using Assets.Script.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Script
{
    public abstract class SphereComponent
    {
        public Transform root;
        public Sphere sphere;
        public SphereComponent(Sphere sphere)
        {
            this.sphere=sphere;
        }

        public void SetRoot(Transform root)
        {
            this.root = root;
        }
    }
}