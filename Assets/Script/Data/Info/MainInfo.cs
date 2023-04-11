using Assets.Script.Data;

namespace Assets.Script
{
    public class MainInfo:InfoBase<MainInfo>
    {
        protected override string GetJsonName()
        {
            return "main";
        }

        public string mainSphere
        {
            get
            {
                return reader.ReadStr("main_sphere");
            }
        }

        public string mainBackground
        {
            get
            {
                return reader.ReadStr("main_background");
            }
        }

        public string mainCamera
        {
            get
            {
                return reader.ReadStr("main_camera");
            }
        }

        public string subCamera
        {
            get
            {
                return reader.ReadStr("sub_camera");
            }
        }
    }
}