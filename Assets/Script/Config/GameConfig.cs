namespace Assets.Script.Config
{
    public static class GameConfig
    {
        private static Backgrounds_Config background;

        public static Backgrounds_Config BackGround
        {
            get
            {
                if (background==null)
                {
                    background = new Backgrounds_Config();
                    background.Init();
                }
                return background;
            }
        }
    }
}