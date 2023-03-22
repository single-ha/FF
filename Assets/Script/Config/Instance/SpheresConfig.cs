using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class SpheresConfig:Config<SpheresConfig>
    {
        public class SphereConfig : ConfigNode
        {
            public int Level
            {
                get
                {
                    return ReadInt("level");
                }
            }
            public string Terrain
            {
                get
                {
                    return ReadStr("terrain");
                }
            }

            private string[] buildings;
            public string[] Buildings
            {
                get
                {
                    ReadArray(ref buildings, "buildings");
                    return buildings;
                }
            }
            private int[] buildings_x;
            public int[] Buildings_X
            {
                get
                {
                    ReadArray(ref buildings_x, "buildings_x");
                    return buildings_x;
                }
            }
            private int[] buildings_y;
            public int[] Buildings_Y
            {
                get
                {
                    ReadArray(ref buildings_y, "buildings_y");
                    return buildings_y;
                }
            }
        }
        protected override void SetConfigName(ref string configName)
        {
            configName = "Spheres";
        }

        private Dictionary<string, SphereConfig> spheres;
        public Dictionary<string, SphereConfig> Spheres
        {
            get
            {
                ReadDictionary(ref spheres);
                return spheres;
            }
        }

        public static SphereConfig GetConfig(string id)
        {
            if (Inst.Spheres.ContainsKey(id))
            {
                return Inst.Spheres[id];
            }
            else
            {
                Debuger.LogError($"spheres 配置表中未配置id为{id}的sphere");
                return null;
            }
        }

    }


}