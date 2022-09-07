using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace Assets.Script
{
    public class Tool
    {
        public static T GetComponent<T>(GameObject root, string path) where T : Component
        {
            if (root==null)
            {
                return null;
            }

            return root.transform.Find(path).GetComponent<T>();
        }
    }
}