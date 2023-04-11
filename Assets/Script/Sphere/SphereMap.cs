using System.Collections.Generic;
using Assets.Script.Data;
using UnityEngine;

namespace Assets.Script
{
    public class SphereMap
    {
        /// <summary>
        /// 家具的单位长度是地图单位长度的几倍
        /// </summary>
        private const int PRE = 2;
        /// <summary>
        /// 单位格子大小
        /// </summary>
        public const float SphereCell = 0.462f / PRE;

        public int level;

        public int Level => level;

        private int diameter;
        public int Diameter => diameter;
        /// <summary>
        /// 地图高度<x,<y,高度>> 中心为(0,0)
        /// </summary>
        private Dictionary<int, Dictionary<int, int>> mapHeight;

        public Dictionary<int, Dictionary<int, int>> MapHeight => mapHeight;

        private List<BuildingInSphere> buildings;

        public List<BuildingInSphere> Buildings => buildings;

        public SphereMap()
        {
            buildings = new List<BuildingInSphere>();
            mapHeight = new Dictionary<int, Dictionary<int, int>>();
        }

        public void Init(int level)
        {
            this.level = level;
            var sizeConfig = SphereLevel.GetLevel(level.ToString());
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
            int length = Mathf.FloorToInt(radius / SphereMap.SphereCell);
            for (int i = -length; i < length; i++)
            {
                for (int j = -length; j < length; j++)
                {
                    InitMapHeight(i, j);
                    // InitMapHeight(i, -j);
                    // InitMapHeight(-i, j);
                    // InitMapHeight(-i, -j);
                }
            }
        }

        private void InitMapHeight(int x, int y)
        {
            if (EdgeCheck(x, y))
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
        public bool Check(int grid_X,int grid_Y, Vector3 size)
        {
            if (!InMap(grid_X,grid_Y))
            {
                return false;
            }
            else
            {
                int height = mapHeight[grid_X][grid_Y];
                int mid_x = (int)size.x / 2;
                int mid_y = (int)size.z / 2;
                for (int i = grid_X-mid_x; i < grid_X+mid_x; i++)
                {
                    for (int j = grid_Y-mid_y; j <grid_Y+mid_y ; j++)
                    {
                        if (!InMap(i,j))
                        {
                            return false;
                        }
                        else if (height!=mapHeight[i][j])
                        {
                            Debuger.LogWarning($"不在一个高度相同的平面上:grid_X:{grid_X},grid_Y:{grid_Y},h0:{height},h1:{mapHeight[i][j]}");
                            return false;
                        }
                    }
                }

                return true;
            }
        }
        public bool InMap(int grid_X, int grid_Y)
        {
            if (!mapHeight.ContainsKey(grid_X) || !mapHeight[grid_X].ContainsKey(grid_Y))
            {
                Debuger.LogWarning($"超出地图范围:grid_X:{grid_X},grid_Y:{grid_Y}");
                return false;
            }

            if (mapHeight[grid_X][grid_Y]<0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 边缘检测
        /// </summary>
        /// <param name="grid_x">待检测点的很座标</param>
        /// <param name="grid_y">待检测点纵座标</param>
        /// <param name="size">调检测物体的大小</param>
        /// <returns></returns>
        private bool EdgeCheck(int grid_x, int grid_y)
        {
            var tempX = grid_x >= 0 ? grid_x + 1 : grid_x;
            var tempY = grid_y <= 0 ? grid_y : grid_y + 1;
            var pos = SphereMap.GetPositionByGrid(tempX,tempY);

            // var pos = SphereMap.GetPositionByGrid(grid_x, grid_y);
            // pos +=new Vector3(SphereMap.SphereCell / 2,0,SphereMap.SphereCell / 2) ;

            var s = Vector3.Distance(pos, Vector3.zero);
            return 2 * s <= diameter;
        }
        public List<BuildingInSphere> AddBuildings(string[] cBuildings, int[] cBuildingsX, int[] cBuildingsY,int[] cBuildingsR)
        {
            List<BuildingInSphere> result = new List<BuildingInSphere>();
            int length = cBuildings.Length;
            for (int i = 0; i < length; i++)
            {
                var id = cBuildings[i];
                var buildingConfig = new BuildingConfig(id);
                if (buildingConfig!=null)
                {
                    if (Check(cBuildingsX[i],cBuildingsY[i], buildingConfig.GetSize_Ration(cBuildingsR[i])))
                    {
                       var b= AddBuilding(id, cBuildingsX[i], cBuildingsY[i], cBuildingsR[i]);
                       result.Add(b);
                    }
                    else
                    {
                        Debuger.LogWarning($"id为:{id},位置为:x:{cBuildingsX[i]},y:{cBuildingsY[i]}的家具未通过检测,丢弃该家具");
                    }
                }
            }

            return result;
        }

        public void AddBuildingWithChck(string id,Vector2 grid,int rotation)
        {
            var buildingConfig =new BuildingConfig(id);
            if (buildingConfig != null)
            {
                if (!Check(grid, buildingConfig.GetSize_Ration(rotation)))
                {
                    Debuger.LogWarning($"id为:{id},位置为:x:{grid.x},y:{grid.y}的家具未通过检测,丢弃该家具");
                    return;
                }
            }
            AddBuilding(id, grid, rotation);
        }
        public BuildingInSphere AddBuilding(string id, int x, int y,int rotation)
        {
            Vector3 grid = new Vector3(x, 0, y);
            grid.y = mapHeight[x][y];
           return AddBuilding(id,grid, rotation);
        }
        public BuildingInSphere AddBuilding(string id, Vector3 grid,int rotation)
        {
            BuildingInSphere b = new BuildingInSphere(id);
            b.grid = grid;
            b.rotation = rotation;
            this.buildings.Add(b);
            SetMapHeight((int)grid.x, (int)grid.z, b.config.GetSize_Ration(rotation));
            return b;
        }

        public Vector3 SampleRandomPostion()
        {
            var radius = diameter / 2.0f;
            var length =Mathf.FloorToInt(radius / SphereMap.SphereCell);
            int x = Random.Range(-length-1, length+1);
            int y = Random.Range(-length - 1, length + 1);
            return new Vector3(x,0, y);
        }

        public static Vector3 GetPositionByGrid(int x,int y)
        {
            return new Vector3(x * SphereCell, 0,y * SphereCell);
        }

        public static Vector3 GetPositionByGrid(Vector3 grid)
        {
            return GetPositionByGrid((int)grid.x, (int)grid.z);
        }
        public float GetHeightByGrid(int grid_x, int grid_y)
        {
            if (mapHeight.ContainsKey(grid_x)&&mapHeight[grid_x].ContainsKey(grid_y))
            {
                return mapHeight[grid_x][grid_y]* SphereCell;
            }

            return float.NegativeInfinity;
        }
        public static Vector2 GetGridByPosition(Vector3 pos)
        {
            var x = pos.x/SphereCell;
            var z = pos.z / SphereCell;
            return new Vector2(x, z);
        }
    }
}