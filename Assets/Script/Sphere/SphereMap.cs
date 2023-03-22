using System.Collections.Generic;
using Assets.Script.Config;
using UnityEngine;

namespace Assets.Script
{
    public class SphereMap
    {
        /// <summary>
        /// 家具的单位长度是地图单位长度的几倍
        /// </summary>
        public const int PRE = 2;
        /// <summary>
        /// 单位格子大小
        /// </summary>
        public const float SphereCell = 0.463f / PRE;

        public int level;

        public int Level => level;

        private int diameter;
        public int Diameter => diameter;
        private Dictionary<int, Dictionary<int, int>> mapHeight;

        public Dictionary<int, Dictionary<int, int>> MapHeight => mapHeight;

        private List<BuildingInSphereData> buildings;

        public List<BuildingInSphereData> Buildings => buildings;

        public SphereMap()
        {
            buildings = new List<BuildingInSphereData>();
            mapHeight = new Dictionary<int, Dictionary<int, int>>();
        }

        public void Init(int level)
        {
            this.level = level;
            var sizeConfig = Config.TerrainsConfig.GetSizeConfig(level.ToString());
            if (sizeConfig!=null)
            {
                diameter = sizeConfig.Diameter;
            }

            if (diameter <= 0)
            {
                Debuger.LogError($"Terrain level's size less or equal 0(level:{level})");
                return;
            }

            var radius = diameter / 2.0f;
            var length = radius / SphereMap.SphereCell;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    InitMapHeight(i, j);
                    InitMapHeight(i, -j);
                    InitMapHeight(-i, j);
                    InitMapHeight(-i, -j);
                }
            }
        }

        private void InitMapHeight(int x, int y)
        {
            Vector3 normal = new Vector3(1, 0, 1);
            if (EdgeCheck(x, y, normal))
            {
                SetMapHeight(x, y, 0);
            }
            else
            {
                SetMapHeight(x, y, -1);
            }
        }

        public void SetMapHeight(int x, int y, int height)
        {
            if (mapHeight.ContainsKey(x))
            {
                mapHeight[x][y] = height;
            }
            else
            {
                Dictionary<int, int> temp = new Dictionary<int, int>();
                mapHeight[x] = temp;
                mapHeight[x][y] = height;
            }
        }

        public void SetMapHeight(int x, int y, Vector3 size)
        {
            int mid_x = (int)size.x / 2;
            int mid_y = (int)size.y / 2;
            for (int i = x - mid_x; i < x + mid_x; i++)
            {
                for (int j = y - mid_y; j < y + mid_y; j++)
                {
                    SetMapHeight(i, j, (int)size.y);
                }
            }
        }
        public bool Check(Vector2 grid,Vector3 size)
        {
            return Check((int)grid.x, (int)grid.y, size);
        }
        public bool Check(int x,int y, Vector3 size)
        {
            if (!InMap(x,y))
            {
                return false;
            }
            else
            {
                int height = mapHeight[x][y];
                int mid_x = (int)size.x / 2;
                int mid_y = (int)size.y / 2;
                for (int i = x-mid_x; i < x+mid_x; i++)
                {
                    for (int j = y-mid_y; j <y+mid_y ; j++)
                    {
                        if (!InMap(i,j))
                        {
                            return false;
                        }
                        else if (height!=mapHeight[i][j])
                        {
                            Debuger.LogWarning($"不在一个高度相同的平面上:x:{x},y:{y},h0:{height},h1:{mapHeight[i][j]}");
                            return false;
                        }
                    }
                }

                return true;
            }
        }
        public bool InMap(int x, int y)
        {
            if (!mapHeight.ContainsKey(x) || !mapHeight[x].ContainsKey(y))
            {
                Debuger.LogWarning($"超出地图范围:x:{x},y:{y}");
                return false;
            }

            return true;
        }
        /// <summary>
        /// 边缘检测
        /// </summary>
        /// <param name="x">待检测点的很座标</param>
        /// <param name="y">待检测点纵座标</param>
        /// <param name="size">调检测物体的大小</param>
        /// <returns></returns>
        private bool EdgeCheck(int x, int y, Vector3 size)
        {
            var pos = SphereMap.GetPositionByGrid(x, y);
            pos = new Vector3(Mathf.Abs(pos.x), Mathf.Abs(pos.y), Mathf.Abs(pos.z));
            pos += size * SphereMap.SphereCell / 2;
            var s = Vector3.Distance(pos, Vector3.zero);
            return 2 * s <= diameter;
        }
        public void AddBuildings(string[] cBuildings, int[] cBuildingsX, int[] cBuildingsY)
        {
            int length = cBuildings.Length;
            for (int i = 0; i < length; i++)
            {
                var id = cBuildings[i];
                var buildingConfig = BuildingsConfig.GetConfig(id);
                if (buildingConfig!=null)
                {
                    if (Check(cBuildingsX[i],cBuildingsY[i],buildingConfig.Size))
                    {
                        AddBuilding(id, cBuildingsX[i], cBuildingsY[i],false);
                    }
                }
            }
        }

        public void AddBuilding(string id,Vector2 grid,bool check)
        {
            if (check)
            {
                var buildingConfig = BuildingsConfig.GetConfig(id);
                if (buildingConfig!=null)
                {
                    if (!Check(grid, buildingConfig.Size))
                    {
                        return;
                    }
                }
            }
            BuildingInSphereData d = new BuildingInSphereData();
            d.id = id;
            d.grid = grid;
            this.buildings.Add(d);
        }

        public void AddBuilding(string id, int x, int y,bool check=true)
        {
            AddBuilding(id,new Vector2(x,y), check);
            var buildingConfig = BuildingsConfig.GetConfig(id);
            SetMapHeight(x,y,buildingConfig.Size);
        }
        public static Vector3 GetPositionByGrid(int x,int y)
        {

            return new Vector3(x * SphereCell, 0,y * SphereCell);
        }

        public static Vector3 GetPositionByGrid(Vector2 grid)
        {
            return GetPositionByGrid((int)grid.x, (int)grid.y);
        }

        public static Vector2 GetGridByPosition(Vector3 pos)
        {
            var x = pos.x/SphereCell;
            var z = pos.z / SphereCell;
            return new Vector2(x, z);
        }
    }
}