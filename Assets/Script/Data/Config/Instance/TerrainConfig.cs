using System.Collections.Generic;

namespace Assets.Script.Data
{
    public class TerrainConfig:DynamicConfig
    {
        public int id
        {
            get { return reader.ReadInt("id"); }
        }
        public string prefab_name
        {
            get { return reader.ReadStr("prefab_name"); }
        }

        public string material
        {
            get
            {
                return reader.ReadStr("material");
            }
        }

        public TerrainConfig(string configName) : base(configName)
        {
        }
    }
}