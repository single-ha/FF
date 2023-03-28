using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Assets.Script
{
    public class CharacterWalk : AnimBase
    {
        private CharacterWalkData data;

        public CharacterWalk(Character character) : base(character)
        {
        }

        public override void Start(object data = null)
        {
            base.Start(data);
            if (data != null && data is CharacterWalkData)
            {
                this.data = (CharacterWalkData)data;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(this.data.targetPos, out hit, 10f, NavMesh.AllAreas))
                {
                    this.data.targetPos = hit.position;
                }

                character.agent.SetDestination(this.data.targetPos);
            }
            else
            {
                Debuger.LogError("walk data is null");
            }
        }

        public override void Update()
        {
            var dis = character.agent.remainingDistance;
            if (dis < 0.05f)
            {
                if (this.data.callBack == null)
                {
                    controller.Swith(CharacterAni.IDLE);
                }
                else
                {
                    this.data.callBack.Invoke();
                }
            }
        }

        public override void PlayAnim()
        {
            character.PlayAni("walk", true);
        }

        public class CharacterWalkData
        {
            public Vector3 targetPos;
            public UnityAction callBack;
        }
    }
}