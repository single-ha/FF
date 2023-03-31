using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

namespace Assets.Script.Data
{
    public class SphereInfos: InfoBase<SphereInfos>
    {

        private Dictionary<string, SphereInfo> spheres;

        public Dictionary<string, SphereInfo> Shpheres
        {
            get
            {
                reader.ReadDictionary(ref spheres);
                return spheres;
            }
        }
        public override JsonData GetJsonData()
        {
            var path = Path.Combine(Application.persistentDataPath,"info","spheres");
            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                return JsonMapper.ToObject(text);
            }
            else
            {
                return new JsonData();
            }
        }
    }

    public class SphereInfo : ConfigNode
    {

    }
}