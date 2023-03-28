namespace Assets.Script.Config
{
    public class CharacterSkinConfig:DynamicConfig
    {
        public CharacterSkinConfig(string configName) : base(configName)
        {
        }

        public string id
        {
            get
            {
                return ReadStr("id");
            }
        }

        public string name
        {
            get
            {
                return ReadStr("name");
            }
        }

        public string type
        {
            get
            {
                return ReadStr("type");
            }
        }

        public string prefab
        {
            get
            {
                return ReadStr("prefab");
            }
        }
    }
}