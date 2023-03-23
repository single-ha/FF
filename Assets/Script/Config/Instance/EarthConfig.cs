using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Assets.Script.Config
{
    public class EarthConfig:DynamicConfig
    {
        public int id
        {
            get { return ReadInt("id"); }
        }
        public string prefab
        {
            get { return ReadStr("prefab"); }
        }


        public static EarthConfig GetEarthSingleConfig(string id)
        {
            return new EarthConfig(id);
        }

        public EarthConfig(string configName) : base(configName)
        {
        }
    }


}