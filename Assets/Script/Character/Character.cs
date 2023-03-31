using Assets.Script.Data;
using Assets.Script.Manager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

namespace Assets.Script
{
    public enum CharacterActionType
    {
        IDLE,
        WALK,
        LAY,
        RUN,
        SIT,
    }

    public class Character
    {
        private string id;
        private int evo;
        public Sphere sphere;
        public NavMeshAgent agent;

        public GameObject root;
        private CharacterConfig config;
        private CharacterSkinConfig skin;
        public CharacterAI characterAi;
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
            InitAI();
        }

        private void InitAI()
        {
            agent = root.GetComponent<NavMeshAgent>();
            var meshBuildSettings = NavMesh.GetSettingsByIndex(1);
            agent.agentTypeID = meshBuildSettings.agentTypeID;
            characterAi = new CharacterAI(this);
            characterAi.AddAIBase(CharacterAIType.RUNAROUND,new RunAround());
            characterAi.AddAIBase(CharacterAIType.WALKAROUND,new WalkAround());
            characterAi.AddAIBase(CharacterAIType.IDLE,new IdleBehaviour());
            characterAi.AddAIBase(CharacterAIType.PLAYWITHBUILDING, new PlayWithBuilding());
        }


        public void SetAI(bool enable)
        {
            characterAi.SetEnable(enable);
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

        public void SetSphere(Sphere sphere)
        {
            this.sphere = sphere;
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
            characterAi.Destory();
        }

        public void DisEnable()
        {
            if (this.root == null)
            {
                return;
            }
            characterAi.DisEnable();
            this.root.gameObject.SetActive(false);
        }

        public void Enable()
        {
            if (this.root == null)
            {
                return;
            }
            this.root.gameObject.SetActive(true);
            characterAi.Enable();
        }
        public void Warp(Vector3 pos)
        {
            characterAi.Warp(pos);
        }
    }
}