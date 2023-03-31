using System.Collections.Generic;

namespace Assets.Script.Data
{
    public class BackgroundsConfig : DynamicConfig
    {
        public int id
        {
            get { return reader.ReadInt("id"); }
        }


        public string image_bg
        {
            get { return reader.ReadStr("image_bg"); }
        }


        public string prefab_bg
        {
            get { return reader.ReadStr("prefab_bg"); }
        }
        public BackgroundsConfig(string configName) : base(configName)
        {
        }

        public static BackgroundsConfig GetConfig(string s)
        {
            return new BackgroundsConfig(s);
        }
    }
}