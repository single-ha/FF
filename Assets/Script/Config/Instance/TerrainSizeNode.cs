using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class TerrainSize:StaticConfig<TerrainSize>
    {
        protected override void SetConfigName(ref string configName)
        {
            configName = "TerrainSize";
        }

        public Dictionary<string, TerrainSizeNode> size;

        public Dictionary<string, TerrainSizeNode> Size
        {
            get
            {
                ReadDictionary(ref size);
                return size;
            }
        }

        public static TerrainSizeNode GetTerrainSize(string level)
        {
            if (Inst.Size.ContainsKey(level))
            {
                return Inst.Size[level];
            }
            else
            {
                Debuger.LogError($"terrainsSize 配置表中未配置id({level})");
                return null;
            }
        }
    }
    public class TerrainSizeNode : ConfigReader
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