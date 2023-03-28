using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class AnimController
    {
        private Dictionary<CharacterAni, AnimBase> anims;
        private AnimBase curPlay;
        private Coroutine cor;
        public AnimController()
        {
            anims = new Dictionary<CharacterAni, AnimBase>();
            cor= GameMain.Inst.StartCoroutine(UpDate());
        }

        public void AddAnim(CharacterAni key, AnimBase anim)
        {
            anim.SetController(this);
            anims[key] = anim;
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