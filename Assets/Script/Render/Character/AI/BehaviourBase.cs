using UnityEngine.Events;

namespace Assets.Script
{
    public class BehaviourBase
    {
        public Character character;
        public UnityAction complateAction;
        public int weight=1;
        protected bool stoped;
        public BehaviourBase(int weight)
        {
            this.weight = weight;
        }

        public BehaviourBase()
        {
        }

        public virtual void Start()
        {

        }

        public virtual void UpDate()
        {

        }

        public virtual void Stop()
        {

        }

        public virtual void DisEnable()
        {

        }
        public virtual void Destory()
        {

        }
    }
}