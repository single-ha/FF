using System.Collections.Generic;

namespace Assets.Script.Data
{
    public class TerrainConfig:DynamicConfig
    {
        public int id
        {
            get { return reader.ReadInt("id"); }
        }
        public string Prefab
        {
            get { return reader.ReadStr("prefab"); }
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