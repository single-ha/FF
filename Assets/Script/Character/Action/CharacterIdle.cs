namespace Assets.Script
{
    public class CharacterIdle:ActionBase
    {
        public CharacterIdle(Character character) : base(character)
        {
        }

        public override void PlayAnim()
        {
            character.PlayAni("idle",false);
        }
    }
}