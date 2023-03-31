using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Data
{
    public class BuildingConfig:DynamicConfig
    {
        public int Id
        {
            get
            {
                return reader.ReadInt("id");
            }
        }

        public string Prefab
        {
            get
            {
                return reader.ReadStr("prefab");
            }
        }

        public int Size_X
        {
            get
            {
                return reader.ReadInt("size_x");
            }
        }
        /// <summary>
        /// 高度
        /// </summary>
        public int Size_Y
        {
            get
            {
                return reader.ReadInt("size_y");
            }
        }
        public int Size_Z
        {
            get
            {
                return reader.ReadInt("size_z");
            }
        }

        public Vector3 Size
        {
            get
            {
                return new Vector3(Size_X, Size_Y, Size_Z);
            }
        }

        private string[] actions;
        public string[] Actions
        {
            get
            {
                reader.ReadArray(ref actions, "actions");
                return actions;
            }
        }
        public Vector3 GetSize_Ration(int rotation)
        {
            if (rotation / 90 % 2 == 1)
            {
                Vector3 result = new Vector3(Size_Z, Size_Y, Size_X);
                return result;
            }
            return Size;
        }

        public BuildingConfig(string configName) : base(configName)
        {

        }
    }


}