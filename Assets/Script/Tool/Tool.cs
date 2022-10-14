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

        public static Vector3 Array2V3(double[] array)
        {
            if (array==null)
            {
                return Vector3.zero;
            }
            else
            {
                float x = array.Length >= 1 ? (float)array[0] : 0;
                float y = array.Length >= 2 ? (float)array[1] : 0;
                float z = array.Length >= 3 ? (float)array[2] : 0;
                return new Vector3(x, y, z);
            }
        }
        public static Vector3 Array2V3(int[] array)
        {
            if (array == null)
            {
                return Vector3.zero;
            }
            else
            {
                var x = array.Length >= 1 ? array[0] : 0;
                var y = array.Length >= 2 ? array[1] : 0;
                var z = array.Length >= 3 ? array[2] : 0;
                return new Vector3(x, y, z);
            }
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