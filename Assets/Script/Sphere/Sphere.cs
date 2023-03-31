using System;
using System.Collections.Generic;
using Assets.Script.Data;
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
        public SphereMap sphereMap;

        private SphereEditor spereEdtior;
        private SphereRoof roof;
        private SphereBuildindgs _buildindgs;
        private SphereCharacters _characters;
        private SphereCover _cover;
        private SphereWall wall;
        private SphereTerrain _terrain;
        private SphereRing _ring;
        private SphereEarth _earth;
        private SphereTerrainMask terrainMask;
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
            var coverRoot = Tool.GetComponent<Transform>(root, "coverRoot");
            var buildingRoot = Tool.GetComponent<Transform>(root, "buildingRoot");
            var characterRoot = Tool.GetComponent<Transform>(root, "characterRoot");
            var floorRoot = Tool.GetComponent<Transform>(root, "floorRoot");
            var ringRoot = Tool.GetComponent<Transform>(root, "ringRoot");
            var terrainRoot = Tool.GetComponent<Transform>(root, "terrainRoot");
            var earthRoot = Tool.GetComponent<Transform>(root, "earthRoot");
            sphereMap = new SphereMap();
            roof = new SphereRoof(roofRoot);
            _buildindgs = new SphereBuildindgs(buildingRoot);
            _characters = new SphereCharacters(characterRoot);
            _cover = new SphereCover(coverRoot);
             wall = new SphereWall(wallRoot);
            _terrain = new SphereTerrain(terrainRoot);
            _ring = new SphereRing(ringRoot);
            _earth = new SphereEarth(earthRoot);
            terrainMask = new SphereTerrainMask(terrainRoot.gameObject, this);
            spereEdtior = new SphereEditor();
            spereEdtior.RegisterClick(OnClickTerrainOrBuilding);
            spereEdtior.SetLayerMask(LayerMask.GetMask("Building"));
            spereEdtior.Enable = true;
        }
        public void EnableEditor()
        {
            SetCellVisible(true);
        }

        public void DisEnableEditor()
        {
            SetCellVisible(false);
        }

        private int count;
        private void OnClickTerrainOrBuilding(GameObject arg1, Vector3 arg2)
        {
            count++;
            if (count % 2 == 1)
            {
                EnableEditor();
                var pos = arg1.transform.localPosition;
                pos.y += 0.5f;
                arg1.transform.localPosition = pos;
                for (int i = 0; i < _characters.characters.Count; i++)
                {
                    _characters.characters[i].DisEnable();
                }
            }
            else
            {
                DisEnableEditor();
                var pos = arg1.transform.localPosition;
                pos.y -= 0.5f;
                arg1.transform.localPosition = pos;
                for (int i = 0; i < _characters.characters.Count; i++)
                {
                    _characters.characters[i].Enable();
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">sphere配置中的id</param>
        public void SetSphere(string id)
        {
            var sphereConfig = new SphereConfig(id);
            SetSphere(sphereConfig);
        }
        public void SetSphere(SphereTemplate sphereTemplate)
        {
            if (sphereTemplate != null)
            {
                sphereMap.Init(sphereTemplate.Level);
                var c = sphereTemplate;
                AddEarth(c.Earth, c.Level);
                AddBuildings(c.Buildings, c.Buildings_X, c.Buildings_Y, c.Buildings_R);
                AddTerrain(c.Terrain, c.Level);
                AddCharacter(c.CharactersId, c.CharactersEvo);
                AddCover(c.cover,c.Level);
                AddRing(c.ring, c.Level);
                AddRoof(c.roof, c.Level);
                AddWall(c.wall, c.Level);
            }
        }
        #region 顶

        private void AddRoof(string cRoof, int cLevel)
        {
            roof.AddRoof(cRoof, cLevel);
        }
        #endregion
        #region 墙
        private void AddWall(string cWall, int cLevel)
        {
            wall.AddWall(cWall, cLevel);
        }
        #endregion
        #region ring
        private void AddRing(string cRing, int cLevel)
        {
            _ring.AddRing(cRing, cLevel);
        }
        #endregion

        #region 精灵
        private void AddCharacter(string[] cCharacterId, int[] cCharacterEvo)
        {
            if (cCharacterId == null || cCharacterEvo == null || cCharacterId.Length != cCharacterEvo.Length)
            {
                return;
            }

            for (int i = 0; i < cCharacterId.Length; i++)
            {
                string id = cCharacterId[i];
                int evo = cCharacterEvo[i];
                AddCharacter(id, evo);
            }
        }
        private Character AddCharacter(string cCharacterId, int cCharacterEvo)
        {
            Character character = new Character(cCharacterId, cCharacterEvo);
            character.SetSphere(this);
            character.LoadModel();
            _characters.AddCharacter(character,sphereMap.SampleRandomPostion());
            return character;
        }

        #endregion

        #region 家具
        public void AddBuildings(string[] cBuildings, int[] cBuildingsX, int[] cBuildingsY,int[] cBuildingsR)
        {
            sphereMap.AddBuildings(cBuildings, cBuildingsX, cBuildingsY, cBuildingsR);
            ShowBuildings();
        }
        public void ShowBuildings()
        {
            _buildindgs.ShowBuildings(sphereMap.Buildings);
        }
        #endregion

        #region 地面
        public void AddTerrain(string id, int level)
        {
            _terrain.AddTerrain(id, level);
        }
        #endregion

        #region earth
        public void AddEarth(string id, int level)
        {
            _earth.AddEarth(id, level);
        }

        #endregion

        #region cover
        public void AddCover(string id, int level)
        {
            _cover.AddCover(id, level);
        }
        #endregion

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

        public bool Check(int grid_X, int grid_Y, Vector3 size)
        {
            return sphereMap.Check(grid_X, grid_Y, size);
        }
    }
}