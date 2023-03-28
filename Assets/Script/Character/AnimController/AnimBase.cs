using Assets.Script.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;

namespace Assets.Script
{
    public abstract class AnimBase
    {
        protected Character character;
        protected AnimController controller;
        public UnityAction exitAction;
        public AnimBase(Character character)
        {
            this.character = character;
        }

        public void SetController(AnimController con)
        {
            this.controller = con;
        }
        public virtual void Start(object data=null)
        {
            PlayAnim();
        }
        public abstract void PlayAnim();

        public virtual void Update()
        {

        }
        public virtual void Exit()
        {
            exitAction?.Invoke();
        }
    }
}