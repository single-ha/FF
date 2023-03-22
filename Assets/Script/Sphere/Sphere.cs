using System;
using System.Collections.Generic;
using Assets.Script.Config;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class Sphere
    {
        private string defaultName = "SphereRoot";
        private GameObject root;
        public GameObject Root
        {
            get => root;
        }

        public int Level
        {
            get
            {
                return sphereMap.level;
            }
        }
        private SphereMap sphereMap;

        private SphereEditor spereEdtior;

        private SphereTerrain _terrain;
        private SphereTerrainMask terrainMask;
        private SphereBuildindgs _buildindgs;

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
            if (obj == null)
            {
                Debuger.LogError($"{defaultName} prefab is not exit");
            }
            else
            {
                root = GameObject.Instantiate(obj);
            }

            var roofRoot = Tool.GetComponent<Transform>(root, "roofRoot");
            var wallRoot = Tool.GetComponent<Transform>(root, "wallRoot");
            var glassRoot = Tool.GetComponent<Transform>(root, "glassRoot");
            var buildingRoot = Tool.GetComponent<Transform>(root, "buildingRoot");
            var characterRoot = Tool.GetComponent<Transform>(root, "characterRoot");
            var floorRoot = Tool.GetComponent<Transform>(root, "floorRoot");
            var ringRoot = Tool.GetComponent<Transform>(root, "ringRoot");
            var terrainRoot = Tool.GetComponent<Transform>(root, "terrainRoot");
            sphereMap = new SphereMap();
            _terrain = new SphereTerrain(terrainRoot);
            terrainMask = new SphereTerrainMask(terrainRoot.gameObject, this);
            _buildindgs = new SphereBuildindgs(buildingRoot);
            spereEdtior = new SphereEditor();

           
        }

        public void EnableEditor()
        {
            spereEdtior.RegisterClick(EditorClick);
            spereEdtior.Enable = true;
            SetCellVisible(true);
        }

        public void DisEnableEditor()
        {
            spereEdtior.RemoveClick(EditorClick);
            spereEdtior.Enable = false;
            SetCellVisible(false);
        }
        private void EditorClick(GameObject arg1, Vector3 arg2)
        {
            // _buildindgs.SetBuilding(10001,arg2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">sphere配置中的id</param>
        public void SetSphereTemplate(string id)
        {
            var sphereConfig = SpheresConfig.GetConfig(id);
            if (sphereConfig!=null)
            {
                sphereMap.Init(sphereConfig.Level);
                var c = sphereConfig;
                AddTerrain(c.Terrain, sphereConfig.Level);
                AddBuildings(c.Buildings, c.Buildings_X, c.Buildings_Y);
                EnableEditor();
            }
        }

        public void AddBuildings(string[] cBuildings, int[] cBuildingsX, int[] cBuildingsY)
        {
            sphereMap.AddBuildings(cBuildings, cBuildingsX, cBuildingsY);
            RefreshBuildingsShow();
        }

        public void RefreshBuildingsShow()
        {
            _buildindgs.RefreshBuildingsShow(sphereMap.Buildings);
        }
        public void AddTerrain(string id, int level)
        {
            _terrain.AddTerrain(id, level);
        }

        public void SetCellVisible(bool visible)
        {
            if (visible)
            {
                terrainMask.ShowMask(sphereMap.MapHeight);
            }
            else
            {
                terrainMask.HideMask();
            }
        }

        public bool Check(Vector2 grid, Vector3 size)
        {
            return sphereMap.Check(grid, size);
        }

        public bool Check(int x, int y, Vector3 size)
        {
            return sphereMap.Check(x, y, size);
        }
    }
}