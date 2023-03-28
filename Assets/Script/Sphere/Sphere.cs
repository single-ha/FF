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
        private SphereEarth _earth;
        private SphereTerrain _terrain;
        private SphereTerrainMask terrainMask;
        private SphereBuildindgs _buildindgs;
        private SphereCharacters _characters;

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
            var earthRoot = Tool.GetComponent<Transform>(root, "earthRoot");
            sphereMap = new SphereMap();
            _earth = new SphereEarth(earthRoot);
            _terrain = new SphereTerrain(terrainRoot);
            terrainMask = new SphereTerrainMask(terrainRoot.gameObject, this);
            _buildindgs = new SphereBuildindgs(buildingRoot);
            _characters = new SphereCharacters(characterRoot);
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
            for (int i = 0; i < _characters.characters.Count; i++)
            {
                var pos = sphereMap.SampleRandomPostion();
                CharacterWalk.CharacterWalkData d = new CharacterWalk.CharacterWalkData();
                d.targetPos = SphereMap.GetPositionByGrid(pos);
                _characters.characters[i].SwithState(CharacterAni.WALK, d);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">sphere配置中的id</param>
        public void SetSphereTemplate(string id)
        {
            var sphereConfig = SphereConfig.GetConfig(id);
            if (sphereConfig!=null)
            {
                sphereMap.Init(sphereConfig.Level);
                var c = sphereConfig;
                AddEarth(c.Earth,sphereConfig.Level);
                AddBuildings(c.Buildings, c.Buildings_X, c.Buildings_Y,c.Buildings_R);
                AddTerrain(c.Terrain, sphereConfig.Level);
                AddCharacter(c.CharactersId, c.CharactersEvo);
            }
        }
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
                var cha = AddCharacter(id, evo);
            }
        }
        private Character AddCharacter(string cCharacterId, int cCharacterEvo)
        {
            Character character = new Character(cCharacterId, cCharacterEvo);
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