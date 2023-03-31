namespace Assets.Script.Data
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
                return reader.ReadStr("id");
            }
        }

        public string name
        {
            get
            {
                return reader.ReadStr("name");
            }
        }

        public string type
        {
            get
            {
                return reader.ReadStr("type");
            }
        }

        public string prefab
        {
            get
            {
                return reader.ReadStr("prefab");
            }
        }
    }
}