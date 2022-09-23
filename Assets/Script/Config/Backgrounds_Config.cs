using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class Backgrounds_Config : Config
    {
        protected override void SetConfigName(ref string configName)
        {
            configName = "Background";
        }

        private Dictionary<string, Background_Config> backgrounds;

        public Dictionary<string, Background_Config> Backgrounds
        {
            get
            {
                ReadDictionary(ref backgrounds);
                return backgrounds;
            }
        }
    }

    public class Background_Config : ConfigBase
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
    }
}