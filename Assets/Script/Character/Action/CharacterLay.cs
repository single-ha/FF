using UnityEngine;

namespace Assets.Script
{
    public class CharacterLay:ActionBase
    {
        private double duration;
        private Coroutine cor;
        public CharacterLay(Character character) : base(character)
        {
        }


        public override void PlayAnim()
        {
            duration= character.PlayAni("lay1", false);
            if (cor!=null)
            {
                Tool.StopDelayAction(cor);
                cor = null;
            }

            cor = Tool.DelayAction(duration, PlayLay2);
        }

        private void PlayLay2()
        {
            character.PlayAni("lay2", true);
            cor = null;
        }

        public override void Exit()
        {
            if (cor != null)
            {
                Tool.StopDelayAction(cor);
                cor = null;
            }
            duration= character.PlayAni("lay3", true);
            cor = Tool.DelayAction(duration, DOExit);
        }

        private void DOExit()
        {
            exitAction?.Invoke();
            cor = null;
        }
    }
}