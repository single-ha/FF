namespace Assets.Script.AI
{
    public class CharacterAI
    {
        private Character character;
        private bool enable;
        public CharacterAI(Character character)
        {
            this.character = character;
        }

        public void SetEnable(bool enable)
        {
            this.enable = enable;
        }

    }
}