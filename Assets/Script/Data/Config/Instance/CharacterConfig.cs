namespace Assets.Script.Data
{
    public class CharacterConfig:DynamicConfig
    {
        public CharacterConfig(string configName) : base(configName)
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
        private string[] _evo_skin;
        public string[] evo_skin
        {
            get
            {
                reader.ReadArray(ref _evo_skin, "evo_skin");
                 return _evo_skin;
            }
        }
    }
}