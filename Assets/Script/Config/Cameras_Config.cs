using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class Cameras_Config:Config<Cameras_Config>
    {
        public class Camera_Config:ConfigNode
        {
            private double[] _position;

            public double[] position
            {
                get
                {
                    ReadArray(ref _position,"position");
                    return _position;
                }
            }

            private double[] _rotation;

            public double[] rotation
            {
                get
                {
                    ReadArray(ref _rotation, "rotation");
                    return _rotation;
                }
            }

            public double fov
            {
                get
                {
                   return ReadDouble("fov");
                }
            }

            public double min_fov
            {
                get
                {
                    return ReadDouble("min_fov");
                }
            }

            public double max_fov
            {
                get
                {
                    return ReadDouble("max_fov");
                }
            }
        }

        protected override void SetConfigName(ref string configName)
        {
            configName = "Cameras";
        }

        private Dictionary<string, Camera_Config> cameras;

        public Dictionary<string, Camera_Config> Cameras
        {
            get
            {
                ReadDictionary(ref cameras);
                return cameras;
            }
        }
    }
}