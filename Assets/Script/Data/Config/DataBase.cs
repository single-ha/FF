using LitJson;

namespace Assets.Script.Data
{
    public class DataBase
    {
        protected JsonReader reader;
        protected JsonData json;
        public DataBase()
        {
            reader = new JsonReader();
        }

        public virtual void SetJson(JsonData js)
        {
            if (reader==null)
            {
                reader = new JsonReader();
            }

            json = js;
            reader.SetJson(json);
        }
    }
}