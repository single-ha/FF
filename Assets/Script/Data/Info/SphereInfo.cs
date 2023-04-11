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
        protected override string GetJsonName()
        {
            return "spheres";
        }
    }

    public class SphereInfo : DataBase
    {
        private SphereTemplate sphereTemplate;

        public SphereTemplate SphereTemplate
        {
            get
            {
                if (sphereTemplate==null)
                {
                    sphereTemplate = new SphereTemplate(json);
                }

                return sphereTemplate;
            }
        }
    }
}