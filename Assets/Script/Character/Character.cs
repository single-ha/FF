using Assets.Script.Config;
using Assets.Script.Manager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

namespace Assets.Script
{
    public enum CharacterAni
    {
        IDLE,
        WALK,
        LAY,
    }

    public class Character
    {
        private string id;
        private int evo;

        public GameObject root;
        public bool ai;
        private CharacterConfig config;
        private CharacterSkinConfig skin;
        public NavMeshAgent agent;
        private ActionController controller;
        public Character(string id, int evo)
        {
            this.id = id;
            this.evo = evo;
            config = new CharacterConfig(id);
            skin = new CharacterSkinConfig(config.evo_skin[evo]);
        }

        public void LoadModel()
        {
            GameObject obj = ResManager.Inst.Load<GameObject>($"{skin.prefab}.prefab");
            root = GameObject.Instantiate(obj);
            agent = root.GetComponent<NavMeshAgent>();
            var meshBuildSettings = NavMesh.GetSettingsByIndex(1);
            agent.agentTypeID = meshBuildSettings.agentTypeID;
            controller = new ActionController();
            controller.AddAnim(CharacterAni.IDLE, new CharacterIdle(this));
            controller.AddAnim(CharacterAni.WALK, new CharacterWalk(this));
            controller.AddAnim(CharacterAni.LAY,new CharacterLay(this));
            controller.Swith(CharacterAni.IDLE);
        }

        public void SetAI(bool enable)
        {
            if (agent==null)
            {
                return;
            }
            agent.enabled = enable;
        }
        public void SetParent(GameObject parent)
        {
            if (root==null||parent==null)
            {
                Debuger.LogWarning($"{id}_{evo}设置父物体失败,root:{root},parent:{parent}");
                return;
            }
            root.transform.SetParent(parent.transform);
            root.transform.localPosition=Vector3.zero;
            root.transform.localScale=Vector3.one;
        }

        public void SwithState(CharacterAni state, object data=null)
        {
            controller.Swith(state,data);
        }
        public double PlayAni(string ani,bool loop)
        {
            if (this.root==null)
            {
                Debuger.LogWarning($"{id}_{evo}播放动画失败,root等于null");
                return 0;
            }
            PlayableDirector pd = this.root.GetComponent<PlayableDirector>();
            if (pd==null)
            {
                pd = this.root.AddComponent<PlayableDirector>();
            }

            pd.extrapolationMode = loop ? DirectorWrapMode.Loop : DirectorWrapMode.Hold;
            PlayableAsset pa = ResManager.Inst.Load<PlayableAsset>($"{skin.type}_{ani}.playable");
            if (pa==null)
            {
                Debuger.LogWarning($"{id}_{evo}播放动画失败,不存在:{skin.type}_{ani}");
                return 0;
            }

            pd.playableAsset = pa;
            Animator animator = this.root.GetComponent<Animator>();
            if (animator==null)
            {
                animator = this.root.AddComponent<Animator>();
            }
            foreach (var assetOutput in pd.playableAsset.outputs)
            {
                if (assetOutput.outputTargetType==typeof(Animator))
                {
                    pd.SetGenericBinding(assetOutput.sourceObject, animator);
                }
            }
            pd.Play();
            return pd.playableAsset.duration;
        }

        public void Destory()
        {
            if (this.root==null)
            {
                return;
            }
            GameObject.Destroy(this.root);
            controller.DisEnable();
        }
        public void Warp(Vector3 pos)
        {
            if (agent==null)
            {
                return;
            }
            agent.Warp(pos);
        }
    }
}