namespace Assets.Script.Config
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
        private string[] _evo_skin;
        public string[] evo_skin
        {
            get
            {
                 ReadArray(ref _evo_skin, "evo_skin");
                 return _evo_skin;
            }
        }
    }
}