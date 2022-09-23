using System.Collections.Generic;
using Assets.Script.Config;
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
            var obj = ResManager.Inst.Load<GameObject>("Stage.prefab");
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
            Tool.ClearChild(main);
            obj.transform.SetParent(main);
            obj.transform.localPosition = Vector3.zero;
        }

        private void SetImageBg(string texture_name)
        {
            if (string.IsNullOrEmpty(texture_name))
            {
                this.imageBgRoot.gameObject.SetActive(false);
            }
            else
            {
                this.imageBgRoot.gameObject.SetActive(true);
                var texture = ResManager.Inst.Load<Texture>($"{texture_name}");
                SetImageBg(texture);
            }
        }

        private void SetPrefabBg(string prefab_name)
        {
            if (string.IsNullOrEmpty(prefab_name))
            {
                this.prefabRoot.gameObject.SetActive(false);
            }
            else
            {
                this.prefabRoot.gameObject.SetActive(true);
                GameObject obj = ResManager.Inst.Load<GameObject>($"{prefab_name}");
                var o = GameObject.Instantiate(obj);
                SetPrefabBg(o);
            }
        }

        public void SetImageBg(Texture texture)
        {
            if (texture == null)
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
            if (prefab == null)
            {
                prefabRoot.gameObject.SetActive(false);
            }
            else
            {
                prefabRoot.gameObject.SetActive(true);
                Tool.ClearChild(prefabRoot);
                prefab.transform.SetParent(prefabRoot);
                prefab.transform.localPosition = Vector3.zero;
                prefab.transform.localScale = Vector3.one;
            }
        }

        public void SetVisible(bool visible)
        {
            root.gameObject.SetActive(visible);
        }

        public void Destory()
        {
            GameObject.Destroy(root);
        }

        public void Decorate(string id)
        {
            var config = GameConfig.BackGround.Backgrounds;
            if (config.ContainsKey(id))
            {
                var c = config[id];
                SetImageBg(c.image_bg);
                SetPrefabBg(c.prefab_bg);
            }
            else
            {
                Debuger.LogError($"stages配置中未包含id为{id}的配置");
            }

            var a = GameConfig.BackGround.Backgrounds;
        }
    }
}