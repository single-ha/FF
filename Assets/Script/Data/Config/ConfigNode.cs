using LitJson;

namespace Assets.Script.Data
{
    public class ConfigNode
    {
        protected JsonReader reader;

        public ConfigNode()
        {
            reader = new JsonReader();
        }

        public void SetJson(JsonData js)
        {
            if (reader==null)
            {
                reader = new JsonReader();
            }
            reader.SetJson(js);
        }
    }
}