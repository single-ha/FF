using System;
using System.Collections.Generic;

namespace Assets.Script.Manager
{
    public class ObjectPoolManager : Instance<ObjectPoolManager>, IManager
    {
        public Dictionary<Type, Queue<IObjeck>> pool;

        public void OnEnable()
        {
            if (pool == null)
            {
                pool = new Dictionary<Type, Queue<IObjeck>>();
            }
            else
            {
                pool.Clear();
            }
        }


        public T GetObject<T>() where T : IObjeck
        {
            var t = typeof(T);
            if (pool.ContainsKey(t) && pool[t].Count > 0)
            {
                return (T)pool[t].Dequeue();
            }
            else
            {
                var retult = Activator.CreateInstance<T>();
                return retult;
            }
        }

        public void Recycle(IObjeck o)
        {
            var t = o.GetType();
            o.ReSet();
            if (pool.ContainsKey(t))
            {
                pool[t].Enqueue(o);
            }
            else
            {
                var que = new Queue<IObjeck>();
                que.Enqueue(o);
                pool[t] = que;
            }
        }
    }
}