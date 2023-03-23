using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class SphereConfig:DynamicConfig
    {
        public int Level
        {
            get
            {
                return ReadInt("level");
            }
        }

        public string Earth
        {
            get
            {
                return ReadStr("earth");
            }
        }
        public string Terrain
        {
            get
            {
                return ReadStr("terrain");
            }
        }

        private string[] buildings;
        public string[] Buildings
        {
            get
            {
                ReadArray(ref buildings, "buildings");
                return buildings;
            }
        }
        private int[] buildings_x;
        public int[] Buildings_X
        {
            get
            {
                ReadArray(ref buildings_x, "buildings_x");
                return buildings_x;
            }
        }
        private int[] buildings_y;
        public int[] Buildings_Y
        {
            get
            {
                ReadArray(ref buildings_y, "buildings_y");
                return buildings_y;
            }
        }

        public static SphereConfig GetConfig(string id)
        {
            return new SphereConfig(id);
        }

        public SphereConfig(string configName) : base(configName)
        {
        }
    }


}