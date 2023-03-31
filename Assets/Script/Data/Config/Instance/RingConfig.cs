using System.Globalization;

namespace Assets.Script.Data
{
    public class RingConfig:DynamicConfig
    {
        public RingConfig(string configName) : base(configName)
        {

        }
        public int id
        {
            get { return reader.ReadInt("id"); }
        }
        public string prefab
        {
            get { return reader.ReadStr("prefab"); }
        }
    }
}