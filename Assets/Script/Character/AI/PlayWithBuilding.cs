using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class PlayWithBuilding:BehaviourBase
    {
        private float angleSpeed = 5;
        private Coroutine cor;
        private int step;
        private BuildingInSphere building;
        public override void Start()
        {
            List<BuildingInSphere> buildings = new List<BuildingInSphere>();
            for (int i = 0; i < character.sphere.sphereMap.Buildings.Count; i++)
            {
                var b = character.sphere.sphereMap.Buildings[i];
                if (b.config.Actions!=null&&b.config.Actions.Length>0&&!b.bePlaying&&b.f!=null)
                {
                    buildings.Add(b);
                }
            }
            if (buildings.Count<=0)
            {
                complateAction?.Invoke();
                return;
            }
            var index = Random.Range(0, buildings.Count);
            building = buildings[index];
            StopCor();
            cor =GameMain.Inst.StartCoroutine(Play());
            building.bePlaying = true;
        }

        private IEnumerator Play()
        {
            var distance = Vector3.Distance(character.root.transform.position, building.f.position);
            if (distance > 0.1f)
            {
                step = 1;
                character.PlayAni("walk", true);
                character.agent.SetDestination(building.f.position);
                yield return new WaitUntil(() => !character.agent.pathPending&& character.agent.remainingDistance <= character.agent.stoppingDistance);
            }

            while (Quaternion.Angle(building.f.rotation, character.root.transform.rotation) > 1.0f)
            {
                character.root.transform.rotation = Quaternion.Slerp(character.root.transform.rotation, building.f.rotation, angleSpeed * Time.deltaTime);
                yield return null;
            }
            step = 2;
            character.root.transform.rotation = building.f.rotation;
            var index = Random.Range(0, building.config.Actions.Length);
            var action = building.config.Actions[index];
            var duration= character.PlayAni($"{action}1", true);
            yield return new WaitForSecondsRealtime((float)duration);
            step = 3;
            character.PlayAni($"{action}2", true);
            duration = Random.Range(1.0f, 5.0f);
            float startTime = Time.realtimeSinceStartup;
            yield return new WaitUntil(() => Time.realtimeSinceStartup - startTime >= duration || stoped);
            step = 4;
            duration =character.PlayAni($"{action}3", true);
            yield return new WaitForSecondsRealtime((float)duration-0.1f);
            step = 5;
            Exit();
        }

        public override void Stop()
        {
            switch (step)
            {
                case 1://寻路
                    Exit();
                    break;
              default:
                  stoped = true;
                  break;
            }
        }

        private void Exit()
        {
            StopCor();
            building.bePlaying = false;
            complateAction?.Invoke();
        }
        private void StopCor()
        {
            if (cor != null)
            {
                GameMain.Inst.StopCoroutine(cor);
                cor = null;
            }
        }
        public override void DisEnable()
        {
            StopCor();
            building.bePlaying = false;
        }
    }
}