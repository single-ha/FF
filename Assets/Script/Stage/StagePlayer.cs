using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class StagePlayer
    {
        public StagePlayer parent;
        public string id;
        public GameObject root;
        public Action onLoaded;

        private Stage stage;

        protected Stage Stage
        {
            get
            {
                if (stage!=null)
                {
                    return stage;
                }

                if (parent!=null)
                {
                    return parent.Stage;
                }

                return null;
            }
        }
        public StagePlayer()
        {
        }

        public StagePlayer(string id)
        {
            this.id = id;
        }

        public virtual void SetStage(Stage stage)
        {
            this.stage = stage;
        }
        public virtual List<StagePlayer> GetStagePlayers()
        {
            return new List<StagePlayer>() { this };
        }

        public virtual void Show()
        {
            if (root == null)
            {
                Load();
                onLoaded?.Invoke();
                OnShow();
            }
            else
            {
                Enable();
            }
        }

        protected virtual void OnShow()
        {
            
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