using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class Backgrounds_Config : Config<Backgrounds_Config>
    {
        protected override void SetConfigName(ref string configName)
        {
            configName = "Backgrounds";
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
        public class Background_Config : ConfigNode
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


}