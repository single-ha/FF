using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Assets.Script.Data
{
    public class EarthConfig:DynamicConfig
    {
        public int id
        {
            get { return reader.ReadInt("id"); }
        }
        public string prefab
        {
            get { return reader.ReadStr("prefab"); }
        }

        public EarthConfig(string configName) : base(configName)
        {
        }
    }


}