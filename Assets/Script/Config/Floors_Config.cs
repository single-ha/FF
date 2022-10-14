using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class Floors_Config:Config<Floors_Config>
    {
        protected override void SetConfigName(ref string configName)
        {
            configName = "Floors";
        }

        private Dictionary<string, Floor_config> floors;

        public Dictionary<string, Floor_config> Floors
        {
            get
            {
                ReadDictionary(ref floors);
                return floors;
            }
        }
        public class Floor_config : ConfigNode
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
    }


}