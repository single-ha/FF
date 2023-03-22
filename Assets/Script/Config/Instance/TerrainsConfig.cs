using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class TerrainsConfig:Config<TerrainsConfig>
    {
        protected override void SetConfigName(ref string configName)
        {
            configName = "Terrains";
        }

        private Dictionary<string, TerrainConfig> terrains;

        public Dictionary<string, TerrainConfig> Terrains
        {
            get
            {
                ReadDictionary(ref terrains,"terrains");
                return terrains;
            }
        }
        public static TerrainConfig GetTerrainConfig(string id)
        {
            if (Inst.Terrains.ContainsKey(id))
            {
                return Inst.Terrains[id];
            }
            else
            {
                Debuger.LogError($"terrains 配置表中未配置id为{id}的terrain");
                return null;
            }
        }
        public Dictionary<string, TerrainSize> size;

        public Dictionary<string, TerrainSize> Size
        {
            get
            {
                ReadDictionary(ref size,"size");
                return size;
            }
        }
        public static TerrainSize GetSizeConfig(string level)
        {
            if (Inst.Size.ContainsKey(level))
            {
                return Inst.Size[level];
            }
            else
            {
                Debuger.LogError($"terrain 配置表中未配置id为{level}的size");
                return null;
            }
        }

        public class TerrainConfig : ConfigNode
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
        public class TerrainSize :ConfigNode
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