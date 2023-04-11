using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script
{
    public class WalkAround:BehaviourBase
    {
        public override void Start()
        {
            var grid = character.sphere.sphereMap.SampleRandomPostion();
            var pos = SphereMap.GetPositionByGrid(grid);
            pos=character.sphere.GetWordPos(pos);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(pos, out hit, 10f, NavMesh.AllAreas))
            {
                pos = hit.position;
            }

            character.agent.speed = 0.5f;
            character.agent.SetDestination(pos);
            character.PlayAni("walk",true);

        }

        public override void UpDate()
        {
            var dis = character.agent.remainingDistance;
            if (!character.agent.pathPending&&dis <=character.agent.stoppingDistance)
            {
                complateAction?.Invoke();
            }
        }

        public override void Stop()
        {
            character.PlayAni("idle", true);
        }
    }
}