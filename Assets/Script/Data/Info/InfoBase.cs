using Assets.Script.Manager;
using LitJson;
using UnityEngine;

namespace Assets.Script.Data
{
    public abstract class InfoBase<T> where T : new()
    {
        private static T _inst;
        protected JsonReader reader;

        public static T Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new T();
                }

                return _inst;
            }
        }

        protected InfoBase()
        {
            var j = GetJsonData();
            SetJson(j);
        }

        public bool dirty = true;
        protected void SetDirty(bool dirty)
        {
            this.dirty = dirty;
        }
        public void SetJson(JsonData json)
        {
            if (reader == null)
            {
                reader = new JsonReader();
            }
            reader.SetJson(json);
            SetDirty(true);
        }
        public abstract JsonData GetJsonData();
    }
}