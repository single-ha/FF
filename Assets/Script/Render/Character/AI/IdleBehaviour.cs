using UnityEngine;

namespace Assets.Script
{
    public class IdleBehaviour: BehaviourBase
    {
        private float duration;
        private float startTime;
        public override void Start()
        {
            duration = Random.Range(1.0f, 5.0f);
            startTime = Time.realtimeSinceStartup;
            character.PlayAni("idle", true);
        }

        public override void UpDate()
        {
            if (Time.realtimeSinceStartup-startTime>=duration)
            {
                complateAction?.Invoke();
            }
        }
    }
}