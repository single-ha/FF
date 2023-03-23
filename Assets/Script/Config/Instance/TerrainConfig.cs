using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class TerrainConfig:DynamicConfig
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

        public static TerrainConfig GetTerrainConfig(string id)
        {
            return new TerrainConfig(id);
        }
        public TerrainConfig(string configName) : base(configName)
        {
        }
    }
}