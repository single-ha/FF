using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace Assets.Script
{
    public class Tool
    {
        public static void ClearChild(Transform root)
        {
            if (root == null)
            {
                return;
            }

            foreach (Transform child in root)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        public static void ClearChild(GameObject root)
        {
            if (root == null)
            {
                return;
            }
            ClearChild(root.transform);
        }
        public static T GetComponent<T>(GameObject root, string path) where T : Component
        {
            if (root==null)
            {
                return null;
            }
            Transform t = root.transform.Find(path);
            if (t == null)
            {
                return null;
            }
            else
            {
                return t.GetComponent<T>();
            }
        }
    }
}