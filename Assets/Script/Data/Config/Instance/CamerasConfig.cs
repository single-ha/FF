using System.Collections.Generic;

namespace Assets.Script.Data
{
    public class CamerasConfig: StaticConfig<CamerasConfig>
    {
        protected override void ConfigName()
        {
            configName = "cameras";
        }

        public class CameraConfig:ConfigNode
        {
            private double[] _position;
            public double[] position
            {
                get
                {
                    reader.ReadArray(ref _position,"position");
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
                    reader.ReadArray(ref _rotation, "rotation");
                    return _rotation;
                }
            }

            public double fov
            {
                get
                {
                    return reader.ReadDouble("fov");
                }
            }

            public double min_fov
            {
                get
                {
                    return reader.ReadDouble("min_fov");
                }
            }

            public double max_fov
            {
                get
                {
                    return reader.ReadDouble("max_fov");
                }
            }
        }


        private Dictionary<string, CameraConfig> cameras;

        public Dictionary<string, CameraConfig> Cameras
        {
            get
            {
                reader.ReadDictionary(ref cameras);
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