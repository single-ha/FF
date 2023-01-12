using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class Spheres_Config:Config<Spheres_Config>
    {
        protected override void SetConfigName(ref string configName)
        {
            configName = "Spheres";
        }

        private Dictionary<string, Sphere_Config> spheres;
        public Dictionary<string, Sphere_Config> Spheres
        {
            get
            {
                ReadDictionary(ref spheres);
                return spheres;
            }
        }


        public class Sphere_Config : ConfigNode
        {
            public string Terrain
            {
                get
                {
                    return ReadStr("terrain");
                }
            }
        }
    }


}