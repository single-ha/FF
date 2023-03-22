using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Config
{
    public class BuildingsConfig:Config<BuildingsConfig>
    {
        public class BuildingConfig : ConfigNode
        {

            public int Id
            {
                get
                {
                    return ReadInt("id");
                }
            }

            public string Prefab
            {
                get
                {
                    return ReadStr("prefab");
                }
            }

            public int Size_X
            {
                get
                {
                    return ReadInt("size_x");
                }
            }
            /// <summary>
            /// 高度
            /// </summary>
            public int Size_Y
            {
                get
                {
                    return ReadInt("size_y");
                }
            }
            public int Size_Z
            {
                get
                {
                    return ReadInt("size_z");
                }
            }

            public Vector3 Size
            {
                get
                {
                    return new Vector3(Size_X, Size_Y, Size_Z);
                }
            }
        }
        protected override void SetConfigName(ref string configName)
        {
            configName = "Buildings";
        }

        private Dictionary<string, BuildingConfig> buildings;

        public Dictionary<string, BuildingConfig> Buildings
        {
            get
            {
                ReadDictionary(ref buildings);
                return buildings;
            }
        }

        public static BuildingConfig GetConfig(string id)
        {
            if (Inst.Buildings.ContainsKey(id))
            {
                return Inst.Buildings[id];
            }
            else
            {
                Debuger.LogError($"buildings 配置表中未配置id为{id}的building");
                return null;
            }
        }
    }


}