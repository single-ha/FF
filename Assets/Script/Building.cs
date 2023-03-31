using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script
{
    public class Building
    {
        public string id;
        public BuildingConfig config;

        public GameObject root;
        public Transform f;
        public Building(string id)
        {
            this.id = id;
            config = new BuildingConfig(id);
        }

        public void Show()
        {
            if (root==null)
            {
                string path = $"{config.Prefab}.prefab";
                GameObject o = ResManager.Inst.Load<GameObject>(path);
                root = GameObject.Instantiate(o);
                f = root.transform.Find("f");
            }
            else
            {
                this.root.gameObject.SetActive(true);
            }
        }

        public void SetNavObstacleAndCollider()
        {
            if (root==null)
            {
                return;
            }
            NavMeshObstacle navMeshObstacle = this.root.GetComponent<NavMeshObstacle>();
            if (navMeshObstacle==null)
            {
                navMeshObstacle = this.root.AddComponent<NavMeshObstacle>();
            }
            navMeshObstacle.carving = true;
            var size = config.Size * SphereMap.SphereCell;
            navMeshObstacle.center = new Vector3(0, size.y / 2, 0);
            navMeshObstacle.size = size;
            BoxCollider collider = this.root.GetComponent<BoxCollider>();
            if (collider==null)
            {
                collider = this.root.AddComponent<BoxCollider>();
            }

            collider.center = new Vector3(0, size.y / 2, 0);
            collider.size = size;
        }

        public void SetLayer(string layer)
        {
            if (root==null)
            {
                return;
            }
            root.SetLayer(layer);
        }
    }
}