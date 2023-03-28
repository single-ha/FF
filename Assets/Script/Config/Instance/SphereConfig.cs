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

        private int[] buildings_r;

        public int[] Buildings_R
        {
            get
            {
                ReadArray(ref buildings_r, "buildings_r");
                return buildings_r;
            }
        }
        private string[] characters_id;

        public string[] CharactersId
        {
            get
            {
                ReadArray(ref characters_id, "characters_id");
                return characters_id;
            }
        }
        private int[] characters_evo;

        public int[] CharactersEvo
        {
            get
            {
                ReadArray(ref characters_evo, "characters_evo");
                return characters_evo;
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