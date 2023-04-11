using System;
using System.Collections.Generic;
using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class Sphere:StagePlayer
    {
        public const string defaultName = "SphereRoot";
        public int level;
        public SphereMap sphereMap;
        private SphereEditor spereEdtior;
        private SphereRoof roof;
        private SphereBuildindgs buildindgs;
        private SphereCharacters characters;
        private SphereCover cover;
        private SphereWall wall;
        private SphereTerrain terrain;
        private SphereRing ring;
        private SphereEarth earth;
        private SphereTerrainMask terrainMask;
        private List<StagePlayer> childs;
        public Sphere()
        {
            InitData();
        }
        public Sphere(string id) : base(id)
        {
            InitData();
            SetSphere(id);
        }

        private void InitData()
        {
            roof = new SphereRoof(this);
            buildindgs = new SphereBuildindgs(this);
            characters = new SphereCharacters(this);
            cover = new SphereCover(this);
            wall = new SphereWall(this);
            terrain = new SphereTerrain(this);
            ring = new SphereRing(this);
            earth = new SphereEarth(this);
            sphereMap = new SphereMap();
            childs = new List<StagePlayer>();
            childs.Add(this);
        }
        public override void Load()
        {
            if (string.IsNullOrEmpty(id))
            {
                id = defaultName;
            }
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
            roof.SetRoot(roofRoot);
            wall.SetRoot(wallRoot);
            cover.SetRoot(coverRoot);
            buildindgs.SetRoot(buildingRoot);
            characters.SetRoot(characterRoot);
            ring.SetRoot(ringRoot);
            earth.SetRoot(earthRoot);
            terrain.SetRoot(terrainRoot);

            terrainMask = new SphereTerrainMask(terrainRoot.gameObject, this);
            spereEdtior = new SphereEditor(this);
            spereEdtior.onStartEditor= StartEditor;
            spereEdtior.onEndEditor = EndEditor;
            spereEdtior.Enable = true;
        }
        public void EnableEditor()
        {
        }

        public void DisEnableEditor()
        {
        }

        private void StartEditor()
        {
            if (Stage!=null)
            {
                Stage.cameraController.Enable = false;
            }
            for (int i = 0; i < characters.characters.Count; i++)
            {
                characters.characters[i].DisEnable();
            }
        }
        private void EndEditor()
        {
            for (int i = 0; i < characters.characters.Count; i++)
            {
                characters.characters[i].Enable();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">sphere配置中的id</param>
        public void SetSphere(string id)
        {
            var sphereConfig = new SphereConfig(id);
            SetSphere(sphereConfig.SphereTemplate);
        }
        public void SetSphere(SphereTemplate sphereTemplate)
        {
            if (sphereTemplate != null)
            {
                sphereMap.Init(sphereTemplate.Level);
                var c = sphereTemplate;
                this.level = c.Level;
                childs.Add(AddEarth(c.Earth, c.Level));
                childs.AddRange(AddBuildings(c.Buildings, c.Buildings_X, c.Buildings_Y, c.Buildings_R));
                childs.Add(AddTerrain(c.Terrain, c.Level));
                childs.AddRange(AddCharacter(c.CharactersId, c.CharactersEvo));
                childs.Add(AddCover(c.cover, c.Level));
                childs.Add(AddRing(c.ring, c.Level)) ;
                childs.Add(AddRoof(c.roof, c.Level));
                childs.Add(AddWall(c.wall, c.Level));
            }
        }
        #region 顶

        private Roof AddRoof(string cRoof, int cLevel)
        {
           return roof.AddRoof(cRoof);
        }
        #endregion
        #region 墙
        private Wall AddWall(string cWall, int cLevel)
        {
          return  wall.AddWall(cWall);
        }
        #endregion
        #region ring
        private Ring AddRing(string cRing, int cLevel)
        {
            return ring.AddRing(cRing, cLevel);
        }
        #endregion

        #region 精灵
        private List<Character> AddCharacter(string[] cCharacterId, int[] cCharacterEvo)
        {
            List<Character> result = new List<Character>();
            if (cCharacterId == null || cCharacterEvo == null || cCharacterId.Length != cCharacterEvo.Length)
            {
                return result;
            }

            for (int i = 0; i < cCharacterId.Length; i++)
            {
                string id = cCharacterId[i];
                int evo = cCharacterEvo[i];
                var character = AddCharacter(id, evo);
                result.Add(character);
            }

            return result;
        }
        private Character AddCharacter(string cCharacterId, int cCharacterEvo)
        {
            Character character = new Character(cCharacterId, cCharacterEvo);
            character.SetSphere(this);
            characters.AddCharacter(character);
            return character;
        }

        #endregion

        #region 家具
        public List<BuildingInSphere> AddBuildings(string[] cBuildings, int[] cBuildingsX, int[] cBuildingsY,int[] cBuildingsR)
        {
           var result= sphereMap.AddBuildings(cBuildings, cBuildingsX, cBuildingsY, cBuildingsR);
            ShowBuildings();
            return result;
        }
        public void ShowBuildings()
        {
            buildindgs.ShowBuildings(sphereMap.Buildings);
        }

        public BuildingInSphere GetBuilding(GameObject obj)
        {
            return buildindgs.GetBuilding(obj);
        }
        #endregion

        #region 地面
        public Terrain AddTerrain(string id, int level)
        {
           return terrain.AddTerrain(id, level);
        }
        #endregion

        #region earth
        public Earth AddEarth(string id, int level)
        {
           return earth.AddEarth(id);
        }

        #endregion

        #region cover
        public Cover AddCover(string id, int level)
        {
           return cover.AddCover(id, level);
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

        public Vector3 GetWordPos(Vector3 localPos)
        {
            return this.root.transform.TransformPoint(localPos);
        }
        public override List<StagePlayer> GetStagePlayers()
        {
            return childs;
        }
        public override void Enable()
        {
            base.Enable();
            for (int i = 0; i < childs.Count; i++)
            {
                childs[i].Enable();
            }
        }

        public override void DisEnable()
        {
            base.DisEnable();
            for (int i = 0; i < childs.Count; i++)
            {
                childs[i].DisEnable();
            }
        }
    }
}