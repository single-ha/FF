using Assets.Script.Config;
using UnityEngine;

namespace Assets.Script
{
    public class BuildingInSphere:Building
    {
        public Vector2 grid;
        public int rotation;
        public bool bePlaying=false;
        public BuildingInSphere(string id) : base(id)
        {
        }
    }
}