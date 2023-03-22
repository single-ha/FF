using System.Collections.Generic;
using Assets.Script.Config;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class SphereTerrainMask
    {
        private GameObject root;
        private GameObject maskRoot;
        private Vector3 scale;
        Vector3 normal = new Vector3(1, 0, 1);

        private Sphere sphere;

        public SphereTerrainMask(GameObject root, Sphere sphere)
        {
            this.root = root;
            this.sphere = sphere;
            scale = new Vector3(SphereMap.SphereCell, 0, SphereMap.SphereCell);
        }

        public void ShowMask(Dictionary<int, Dictionary<int, int>> mapHeight)
        {
            if (maskRoot == null)
            {
                maskRoot = new GameObject();
                maskRoot.name = "terrainMask";
                maskRoot.transform.SetParent(this.root.transform);
                maskRoot.transform.localPosition = new Vector3(0, 0.1f, 0);
            }
            else
            {
            }

            maskRoot.gameObject.SetActive(true);
            RefreshCells(mapHeight);
        }

        private void RefreshCells(Dictionary<int, Dictionary<int, int>> mapHeight)
        {
            GameObject temp = ResManager.Inst.Load<GameObject>("Cell.prefab");
            foreach (var v0 in mapHeight)
            {
                foreach (var v1 in v0.Value)
                {
                    if (v1.Value >= 0)
                    {
                        SetCell(v0.Key, v1.Key, temp);
                    }
                }
            }
        }

        private void SetCell(int i, int j, GameObject temp)
        {
            string name = $"{i}_{j}";
            Transform obj = maskRoot.transform.Find(name);
            if (obj == null)
            {
                obj = GameObject.Instantiate(temp, maskRoot.transform).transform;
                obj.localPosition = SphereMap.GetPositionByGrid(i, j);
                obj.localScale = scale;
                obj.name = name;
            }

            obj.gameObject.SetActive(sphere.Check(i, j, normal));
        }

        public void HideMask()
        {
            if (maskRoot != null)
            {
                maskRoot.gameObject.SetActive(false);
            }
        }
    }
}