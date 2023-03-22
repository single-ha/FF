using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class BackgroundsConfig : Config<BackgroundsConfig>
    {
        public class BackgroundConfig : ConfigNode
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
        protected override void SetConfigName(ref string configName)
        {
            configName = "Backgrounds";
        }

        private Dictionary<string, BackgroundConfig> backgrounds;

        public Dictionary<string, BackgroundConfig> Backgrounds
        {
            get
            {
                ReadDictionary(ref backgrounds);
                return backgrounds;
            }
        }
        public static BackgroundConfig GetConfig(string id)
        {
            if (Inst.Backgrounds.ContainsKey(id))
            {
                return Inst.Backgrounds[id];
            }
            else
            {
                Debuger.LogError($"backgrounds 配置表中未配置id为{id}的background");
                return null;
            }
        }
    }
}