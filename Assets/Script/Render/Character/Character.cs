using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

namespace Assets.Script
{
    public class Character : StagePlayer
    {
        private int evo;
        public Sphere sphere;
        public NavMeshAgent agent;

        private CharacterConfig config;
        private CharacterSkinConfig skin;
        public CharacterAI characterAi;

        public Character(string id, int evo) : base(id)
        {
            this.id = id;
            this.evo = evo;
            config = new CharacterConfig(id);
            skin = new CharacterSkinConfig(config.evo_skin[evo]);
        }

        public override void Load()
        {
            GameObject obj = ResManager.Inst.Load<GameObject>($"{skin.prefab}.prefab");
            root = GameObject.Instantiate(obj);
            InitAI();
        }

        private void InitAI()
        {
            agent = root.GetComponent<NavMeshAgent>();
            var meshBuildSettings = NavMesh.GetSettingsByIndex(1);
            agent.agentTypeID = meshBuildSettings.agentTypeID;
            characterAi = new CharacterAI(this);
            characterAi.AddAIBase(CharacterAIType.RUNAROUND, new RunAround());
            characterAi.AddAIBase(CharacterAIType.WALKAROUND, new WalkAround());
            characterAi.AddAIBase(CharacterAIType.IDLE, new IdleBehaviour());
            characterAi.AddAIBase(CharacterAIType.PLAYWITHBUILDING, new PlayWithBuilding());
        }


        public void SetAI(bool enable)
        {
            characterAi.SetEnable(enable);
        }

        public void SetSphere(Sphere sphere)
        {
            this.sphere = sphere;
        }

        public double PlayAni(string ani, bool loop)
        {
            if (this.root == null)
            {
                Debuger.LogWarning($"{id}_{evo}播放动画失败,root等于null");
                return 0;
            }

            PlayableDirector pd = this.root.GetComponent<PlayableDirector>();
            if (pd == null)
            {
                pd = this.root.AddComponent<PlayableDirector>();
            }

            pd.extrapolationMode = loop ? DirectorWrapMode.Loop : DirectorWrapMode.Hold;
            PlayableAsset pa = ResManager.Inst.Load<PlayableAsset>($"{skin.type}_{ani}.playable");
            if (pa == null)
            {
                Debuger.LogWarning($"{id}_{evo}播放动画失败,不存在:{skin.type}_{ani}");
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

        public void Destory()
        {
            if (this.root == null)
            {
                return;
            }

            GameObject.Destroy(this.root);
            characterAi.Destory();
        }

        public override void DisEnable()
        {
            base.DisEnable();
            characterAi.DisEnable();
        }

        public override void Enable()
        {
            base.Enable();
            characterAi.Enable();
        }

        public void Warp(Vector3 pos)
        {
            characterAi.Warp(pos);
        }
    }
}