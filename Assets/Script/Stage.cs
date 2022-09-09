using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script
{
    public class Stage
    {
        public GameObject root;
        public Camera camera;
        public Transform imageBgRoot;
        public RawImage bg_Image;
        public Transform prefabRoot;
        public Transform main;
        public Transform forword;

        public Stage(GameObject root)
        {
            this.root = root;
            Init();
        }

        public Stage()
        {
            var obj = ResManager.Inst.Load<GameObject>("Stage");
            this.root = GameObject.Instantiate(obj);
            Init();
        }

        private void Init()
        {
            camera = Tool.GetComponent<Camera>(root, "Camera");
            imageBgRoot = Tool.GetComponent<Transform>(root, "BgRoot/ImageBgRoot");
            bg_Image = Tool.GetComponent<RawImage>(root, "BgRoot/ImageBgRoot/Image");
            prefabRoot = Tool.GetComponent<Transform>(root, "BgRoot/PrefabRoot");
            main = Tool.GetComponent<Transform>(root, "Main");
            forword = Tool.GetComponent<Transform>(root, "Forword");
        }

        public void SetMainStage()
        {
            this.camera.tag = "MainCamera";
        }
        public void ShowGameObject(GameObject obj)
        {
            Tool.ClearChild(obj);
            obj.transform.SetParent(main);
            obj.transform.localPosition=Vector3.zero;
        }

        public void SetImageBg(Texture texture)
        {
            if (texture==null)   
            {
                imageBgRoot.gameObject.SetActive(false);
            }
            else
            {
                imageBgRoot.gameObject.SetActive(true);
                bg_Image.texture = texture;
                bg_Image.SetNativeSize();
            }
        }

        public void SetPrefabBg(GameObject prefab)
        {
            Tool.ClearChild(prefabRoot);
            prefab.transform.SetParent(prefabRoot);
            prefab.transform.localPosition=Vector3.zero;
        }
        public void SetVisible(bool visible)
        {
            root.gameObject.SetActive(visible);
        }

        public void Destory()
        {
            GameObject.Destroy(root);
        }
    }
}