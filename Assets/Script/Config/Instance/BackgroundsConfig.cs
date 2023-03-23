using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class BackgroundsConfig : DynamicConfig
    {
        public int id
        {
            get { return ReadInt("id"); }
        }


        public string image_bg
        {
            get { return ReadStr("image_bg"); }
        }


        public string prefab_bg
        {
            get { return ReadStr("prefab_bg"); }
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