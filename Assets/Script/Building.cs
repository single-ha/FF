using Assets.Script.Config;
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

        public void SetNavObstacle()
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
            navMeshObstacle.size = config.Size*SphereMap.SphereCell;
        }
    }
}