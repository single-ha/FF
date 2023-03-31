namespace Assets.Script.Data
{
    public class RoofConfig:DynamicConfig
    {
        public RoofConfig(string configName) : base(configName)
        {
        }
        public string ID
        {
            get
            {
                return reader.ReadStr("id");
            }
        }
        public string Prefab
        {
            get
            {
                return reader.ReadStr("prefab");
            }
        }
    }
}