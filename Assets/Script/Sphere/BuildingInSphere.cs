using UnityEngine;

namespace Assets.Script
{
    public class BuildingInSphere:Building
    {
        /// <summary>
        /// 
        /// </summary>
        public Vector3 grid;
        public int rotation;
        public bool bePlaying=false;
        public BuildingInSphere(string id) : base(id)
        {
        }
    }
}