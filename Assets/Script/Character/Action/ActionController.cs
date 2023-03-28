using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class ActionController
    {
        private Dictionary<CharacterAni, ActionBase> anims;
        private ActionBase curPlay;
        private Coroutine cor;
        public ActionController()
        {
            anims = new Dictionary<CharacterAni, ActionBase>();
            cor= GameMain.Inst.StartCoroutine(UpDate());
        }

        public void AddAnim(CharacterAni key, ActionBase action)
        {
            action.SetController(this);
            anims[key] = action;
        }

        public void Swith(CharacterAni key,object data=null)
        {
            if (!anims.ContainsKey(key))
            {
                Debuger.LogError($"不支持的动作类型:{key.ToString()}");
                return;
            }

            var next = anims[key];
            // if (next==curPlay)
            // {
            //     return;
            // }
            if (curPlay!=null)
            {
                curPlay.exitAction = delegate()
                {
                    curPlay = next;
                    curPlay.Start(data);
                };
                curPlay.Exit();
            }
            else
            {
                curPlay = next;
                curPlay.Start(data);
            }
        }

        private IEnumerator UpDate()
        {
            while (true)
            {
                if (curPlay!=null)
                {
                    curPlay.Update();
                }

                yield return null;
            }
        }

        public void DisEnable()
        {
            GameMain.Inst.StopCoroutine(cor);
            cor = null;
        }
    }
}