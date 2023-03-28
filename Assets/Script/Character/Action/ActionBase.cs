using Assets.Script.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;

namespace Assets.Script
{
    public abstract class ActionBase
    {
        protected Character character;
        protected ActionController controller;
        public UnityAction exitAction;
        public ActionBase(Character character)
        {
            this.character = character;
        }

        public void SetController(ActionController con)
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