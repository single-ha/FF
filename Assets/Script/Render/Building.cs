using System;
using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

namespace Assets.Script
{
    public class Building:StagePlayer
    {
        public BuildingConfig config;

        public Transform f;
        public Building(string id):base(id)
        {
            this.id = id;
            config = new BuildingConfig(id);
        }

        public override void Load()
        {
            string path = $"{config.Prefab}.prefab";
            GameObject o = ResManager.Inst.Load<GameObject>(path);
            root = GameObject.Instantiate(o);
            f = root.transform.Find("f");
        }
        public void SetNavObstacleAndCollider()
        {
            if (root==null)
            {
                return;
            }
            NavMeshObstacle navMeshObstacle = this.root.GetComponent<NavMeshObstacle>();
            if (navMeshObstacle==null)
            {
                navMeshObstacle = this.root.AddComponent<NavMeshObstacle>();
            }
            navMeshObstacle.carving = true;
            var size = config.Size * SphereMap.SphereCell;
            navMeshObstacle.center = new Vector3(0, size.y / 2, 0);
            navMeshObstacle.size = size;
            BoxCollider collider = this.root.GetComponent<BoxCollider>();
            if (collider==null)
            {
                collider = this.root.AddComponent<BoxCollider>();
            }

            collider.center = new Vector3(0, size.y / 2, 0);
            collider.size = size;
        }
        public double PlayAni(string buildingselecttimeline, bool loop)
        {
            if (this.root == null)
            {
                Debuger.LogWarning($"building{id}播放动画失败,root等于null");
                return 0;
            }
            PlayableDirector pd = this.root.GetComponent<PlayableDirector>();
            if (pd == null)
            {
                pd = this.root.AddComponent<PlayableDirector>();
            }

            pd.enabled = true;
            pd.extrapolationMode = loop ? DirectorWrapMode.Loop : DirectorWrapMode.Hold;
            PlayableAsset pa = ResManager.Inst.Load<PlayableAsset>($"{buildingselecttimeline}.playable");
            if (pa == null)
            {
                Debuger.LogWarning($"{id}播放动画失败,不存在:{buildingselecttimeline}");
                return 0;
            }

            pd.playableAsset = pa;
            Animator animator = this.root.GetComponent<Animator>();
            if (animator == null)
            {
                animator = this.root.AddComponent<Animator>();
            }
            foreach (var assetOutput in pd.playableAsset.outputs)
            {
                if (assetOutput.outputTargetType == typeof(Animator))
                {
                    pd.SetGenericBinding(assetOutput.sourceObject, animator);
                }
            }
            pd.Play();
            return pd.playableAsset.duration;
        }

        public void Stop()
        {
            if (this.root == null)
            {
                Debuger.LogWarning($"building{id}播放动画失败,root等于null");
                return ;
            }
            PlayableDirector pd = this.root.GetComponent<PlayableDirector>();
            if (pd != null)
            {
                pd.Stop();
                pd.enabled = false;
            }
        }

        public void Pause()
        {
            if (this.root == null)
            {
                Debuger.LogWarning($"building{id}播放动画失败,root等于null");
                return;
            }
            PlayableDirector pd = this.root.GetComponent<PlayableDirector>();
            if (pd != null)
            {
                pd.Pause();
            }
        }

        public void Resume()
        {
            if (this.root == null)
            {
                Debuger.LogWarning($"building{id}播放动画失败,root等于null");
                return;
            }
            PlayableDirector pd = this.root.GetComponent<PlayableDirector>();
            if (pd != null)
            {
                pd.Resume();
            }
        }
        public void SetLayer(string layer)
        {
            if (root==null)
            {
                return;
            }
            root.SetLayer(layer);
        }
    }
}