using System.Collections.Generic;
using LitJson;

namespace Assets.Script.Data
{
    public class SphereLevel:StaticConfig<SphereLevel>
    {
        protected override void ConfigName()
        {
            configName = "sphere_level";
        }
        public Dictionary<string, SphereLevelNode> size;
        public Dictionary<string, SphereLevelNode> Size
        {
            get
            {
                reader.ReadDictionary(ref size);
                return size;
            }
        }

        public static SphereLevelNode GetLevel(string level)
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
    public class SphereLevelNode : ConfigNode
    {
        public int Level
        {
            get
            {
                return reader.ReadInt("level");
            }
        }

        public int Diameter
        {
            get
            {
                return reader.ReadInt("diameter");
            }
        }

        public double scale
        {
            get
            {
                return reader.ReadDouble("scale");
            }
        }

        public double roof_scale
        {
            get
            {
                return reader.ReadDouble("roof_scale");
            }
        }
    }
}