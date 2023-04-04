using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Assets.Script
{
    public enum CharacterAIType
    {
        IDLE,
        WALKAROUND,
        RUNAROUND,
        PLAYWITHBUILDING
    }

    public class CharacterAI
    {
        private Character character;

        private bool aiEnable;
        private Dictionary<CharacterAIType, BehaviourBase> allBehaviour;
        private int totalWeight;
        private BehaviourBase curBehaviour;
        private Coroutine cor;
        public CharacterAI(Character character)
        {
            this.character = character;
            allBehaviour = new Dictionary<CharacterAIType, BehaviourBase>();
            totalWeight = 0;
        }

        public void AddAIBase(CharacterAIType type,BehaviourBase behaviour)
        {
            behaviour.character = character;
            behaviour.complateAction = BeginBehaviour;
            totalWeight += behaviour.weight;
            allBehaviour[type]=behaviour;
        } 
        public void SetEnable(bool enable,bool immediate=false)
        {
            if (character.agent == null)
            {
                return;
            }
            this.aiEnable = enable;
            character.agent.enabled = enable;
            if (enable)
            {
                BeginBehaviour();
                cor = GameMain.Inst.StartCoroutine(Update());
            }
            else
            {
                StopAI();
            }
        }

        private void StopAI()
        {
            if (cor != null)
            {
                GameMain.Inst.StopCoroutine(cor);
                cor = null;
            }

            if (curBehaviour!=null)
            {
                curBehaviour.Stop();
                curBehaviour = null;
            }
        }

        private IEnumerator Update()
        {
            while (true)
            {
                if (curBehaviour!=null)
                {
                    curBehaviour.UpDate();
                }
                yield return null;
            }
        }

        private void BeginBehaviour()
        {
            if (!aiEnable)
            {
                StopAI();
                character.PlayAni("idle",true);
                return;
            }
            if (allBehaviour.Count<=0)
            {
                return;
            }
            var weight = Random.Range(0, totalWeight);
            BehaviourBase next = null;
            foreach (var behaviourBase in allBehaviour)
            {
                weight -= behaviourBase.Value.weight;
                if (weight<0)
                {
                    Debuger.Log($"change behaviour:{behaviourBase.Key}");
                    next = behaviourBase.Value;
                    break;
                }
            }

            if (next!=null)
            {
                curBehaviour = next;
                curBehaviour.Start();
            }
            else
            {
                Debuger.LogError("未随机到可执行行为");
            }
        }

        public void BeginBehaviour(BehaviourBase behaviour)
        {
            if (curBehaviour!=null)
            {
                curBehaviour.complateAction = delegate()
                {
                    behaviour.Start();
                    curBehaviour = behaviour;
                };
                curBehaviour.Stop();
            }
            else
            {
                behaviour.Start();
                curBehaviour = behaviour;
            }
        }
        public void DisEnable()
        {
            if (cor != null)
            {
                GameMain.Inst.StopCoroutine(cor);
                cor = null;
            }
            if (curBehaviour != null)
            {
                curBehaviour.DisEnable();
            }
        }

        public void Enable()
        {
            if (aiEnable)
            {
                BeginBehaviour();
                if (cor != null)
                {
                    GameMain.Inst.StopCoroutine(cor);
                    cor = null;
                }
                cor = GameMain.Inst.StartCoroutine(Update());
            }

        }
        public void Destory()
        {
            if (cor != null)
            {
                GameMain.Inst.StopCoroutine(cor);
                cor = null;
            }
            if (curBehaviour != null)
            {
                curBehaviour.Destory();
            }
        }

        public void Warp(Vector3 pos)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(pos, out hit, 10f, NavMesh.AllAreas))
            {
                character.agent.Warp(hit.position);
            }
            else
            {
                Debuger.LogWarning("未找到代理可移动得点");
            }
        }
    }
}