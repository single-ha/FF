using System.Collections;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script
{
    public static class Tool
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

        public static void SetLayer(this GameObject gameObject, string layerName)
        {
            gameObject.layer = LayerMask.NameToLayer(layerName);
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var g = gameObject.transform.GetChild(i);
                SetLayer(g.gameObject,layerName);
            }
        }
        public static void SetLayer(this GameObject gameObject, int layer)
        {
            gameObject.layer =layer;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var g = gameObject.transform.GetChild(i);
                SetLayer(g.gameObject, layer);
            }
        }

        public static Coroutine DelayAction(double duration, UnityAction action)
        {
            Coroutine result = GameMain.Inst.StartCoroutine(Delay(duration, action));
            return result;
        }

        private static IEnumerator Delay(double duration, UnityAction action)
        {
            yield return new WaitForSecondsRealtime((float)duration);
            action?.Invoke();
        }

        public static void StopDelayAction(Coroutine coroutine)
        {
            GameMain.Inst.StopCoroutine(coroutine);
        }
    }
}