using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Config
{
    public class BuildingConfig:DynamicConfig
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

        public static BuildingConfig GetConfig(string id)
        {
            return new BuildingConfig(id);
        }

        public BuildingConfig(string configName) : base(configName)
        {

        }
    }


}