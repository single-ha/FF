using System.Collections.Generic;

namespace Assets.Script.Data
{
    public class SphereConfig: DynamicConfig
    {
        public SphereConfig(string configName) : base(configName)
        {
        }

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