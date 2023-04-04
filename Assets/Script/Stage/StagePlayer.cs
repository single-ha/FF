using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class StagePlayer
    {
        public string id;

        public GameObject root;
        public Action onLoaded;
        public StagePlayer()
        {
        }

        public StagePlayer(string id)
        {
            this.id = id;
        }

        public virtual List<StagePlayer> GetGraphs()
        {
            return new List<StagePlayer>() { this };
        }

        public virtual void Show()
        {
            if (root == null)
            {
                Load();
                onLoaded?.Invoke();
            }
            else
            {
                Enable();
            }
        }

        public void SetParent(Transform parent)
        {
            if (root!=null)
            {
                this.root.transform.SetParent(parent);
            }
        }
        public virtual void Load()
        {

        }

        public virtual void Enable()
        {
            this.root.gameObject.SetActive(true);
        }

        public virtual void DisEnable()
        {
            this.root.gameObject.SetActive(false);
        }
    }
}