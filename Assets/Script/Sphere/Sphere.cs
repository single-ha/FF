using System.Collections.Generic;
using Assets.Script.Config;
using Lean.Touch;
using UnityEngine;

namespace Assets.Script
{
    public class Sphere
    {
        private string defaultName = "SphereRoot";
        private GameObject root;

        private SphereMap sphereMap;
        public GameObject Root
        {
            get => root;
        }
        private SphereTerrain _terrain;
        public Sphere()
        {
            Init();
        }

        public Sphere(string prefabName)
        {
            defaultName = prefabName;
            Init();
        }
        private void Init()
        {
            var obj = ResManager.Inst.Load<GameObject>($"{defaultName}.prefab");
            if (obj==null)
            {
                Debuger.LogError($"{defaultName} prefab is not exit");
            }
            else
            {
                root = GameObject.Instantiate(obj);
            }
            var roofRoot= Tool.GetComponent<Transform>(root, "roofRoot");
            var wallRoot = Tool.GetComponent<Transform>(root, "wallRoot");
            var glassRoot = Tool.GetComponent<Transform>(root, "glassRoot");
            var buildingRoot = Tool.GetComponent<Transform>(root, "buildingRoot");
            var characterRoot = Tool.GetComponent<Transform>(root, "characterRoot");
            var floorRoot = Tool.GetComponent<Transform>(root, "floorRoot");
            var ringRoot = Tool.GetComponent<Transform>(root, "ringRoot");
            var terrainRoot = Tool.GetComponent<Transform>(root, "terrainRoot");
            _terrain = new SphereTerrain(terrainRoot);
        }

        public void SetSphereTemplate(string id, int level)
        {
            var config = Spheres_Config.Inst.Spheres;
            if (config.ContainsKey(id))
            {
                var c = config[id];
                SetTerrain(c.Terrain, level);
            }
            else
            {
                Debuger.LogError($"spheres 中没有id为{id}的配置");
            }
        }

        public void SetTerrain(string id,int level)
        {
            _terrain.SetComponent(id,level);
            sphereMap = new SphereMap(level);
        }
    }
}