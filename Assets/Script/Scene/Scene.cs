using System.Collections.Generic;
using Assets.Script.Manager;
using UnityEngine;

namespace Assets.Script
{
    public class Scene:StagePlayer
    {
        private Dictionary<int, Transform> spherePos;
        private List<StagePlayer> playerList;
        private Dictionary<int, Sphere> spheres;
        private Transform sphereRoot;
        public Scene()
        {
            playerList = new List<StagePlayer>();
            playerList.Add(this);
            spheres = new Dictionary<int, Sphere>();
            spherePos = new Dictionary<int, Transform>();
        }

        public void AddSphere(int index, Sphere sphere)
        {
            spheres[index] = sphere;
            sphere.onLoaded = delegate()
            {
                OnSphereShow(index);
            };
            playerList.AddRange(sphere.GetStagePlayers());
        }

        protected override void OnShow()
        {
            Stage.SetCamera("1");
            Stage.cameraController.Enable = false;
        }

        private void OnSphereShow(int index)
        {
            spheres[index].SetParent(sphereRoot);
            spheres[index].root.transform.position = spherePos[index].position;
        }
        public override List<StagePlayer> GetStagePlayers()
        {
            return playerList;
        }

        public override void Load()
        {
            var obj = ResManager.Inst.Load<GameObject>("Scene.prefab");
            this.root = GameObject.Instantiate(obj);
            var treeRoot = this.root.transform.Find("tree");
            Tool.ClearChild(treeRoot);
            var tree = ResManager.Inst.Load<GameObject>("buildings@16201.prefab");
            var treeObj = GameObject.Instantiate(tree,treeRoot);
            var sphere0 = treeObj.transform.Find("Anchor/sphere0");
            spherePos[0] = sphere0;
            sphereRoot = this.root.transform.Find("sphere");
        }
    }
}