using System.Collections.Generic;

namespace Assets.Script.Config
{
    public class CamerasConfig: StaticConfig<CamerasConfig>
    {
        public class CameraConfig:ConfigReader
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
            /// <summary>
            /// 摄像机Anchor的旋转角度
            /// </summary>
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

        private Dictionary<string, CameraConfig> cameras;

        public Dictionary<string, CameraConfig> Cameras
        {
            get
            {
                ReadDictionary(ref cameras);
                return cameras;
            }
        }
        public static CameraConfig GetConfig(string id)
        {
            if (Inst.Cameras.ContainsKey(id))
            {
                return Inst.Cameras[id];
            }
            else
            {
                Debuger.LogError($"cameras 配置表中未配置id为{id}的camera");
                return null;
            }
        }
    }
}