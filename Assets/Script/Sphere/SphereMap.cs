using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class SphereMap
    {
        public int level;
        private int radius;
        public static float SphereCell= 0.232f;
        public List<IChecker> checkList;
        public SphereMap(int level)
        {
            this.level = level;
            Init();
            checkList = new List<IChecker>();
            checkList.Add(new EdgeChecker(radius));
        }

        private void Init()
        {
            var config = Config.Terrains_Config.Inst.Size;
            if (config.ContainsKey(level.ToString()))
            {
                radius = config[level.ToString()].Diameter;
            }

            if (radius<=0)
            {
                Debuger.LogError($"Terrain level's size less or equal 0(level:{level})");
                return;
            }
        }

        public static Vector2 GetPositionByGrid(int x,int y)
        {
            return new Vector2(x * SphereCell, y * SphereCell);
        }

        public static Vector2 GetPositionByGrid(Vector2 grid)
        {
            return GetPositionByGrid((int)grid.x, (int)grid.y);
        }
    }
}