using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class Terrains_Config:Config<Terrains_Config>
    {
        protected override void SetConfigName(ref string configName)
        {
            configName = "Terrains";
        }

        private Dictionary<string, Terrain_Config> terrains;

        public Dictionary<string, Terrain_Config> Terrains
        {
            get
            {
                ReadDictionary(ref terrains,"terrains");
                return terrains;
            }
        }

        public Dictionary<string, Terrain_Size> size;

        public Dictionary<string, Terrain_Size> Size
        {
            get
            {
                ReadDictionary(ref size,"size");
                return size;
            }
        }


        public class Terrain_Config : ConfigNode
        {
            public int id
            {
                get { return ReadInt("id"); }
            }
            public string prefab_name
            {
                get { return ReadStr("prefab_name"); }
            }

            public string material
            {
                get
                {
                    return ReadStr("material");
                }
            }
        }
        public class Terrain_Size :ConfigNode
        {
            public int Level
            {
                get
                {
                    return ReadInt("level");
                }
            }

            public int Diameter
            {
                get
                {
                    return ReadInt("diameter");
                }
            }
        }
    }


}