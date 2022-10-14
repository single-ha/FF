using System.Collections.Generic;
using Assets.Script.Config;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Assets.Script
{
    public class Stage : IObjeck
    {
        private GameObject root;
        private Transform cameraAnchor;
        private Camera camera;
        private UniversalAdditionalCameraData cameraData;
        private Transform imageBgRoot;
        private RawImage bg_Image;
        private Transform prefabRoot;
        private Transform main;
        private Transform forword;
        private CameraController cameraController;

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
            cameraAnchor = Tool.GetComponent<Transform>(root, "CameraAnchor");
            camera = Tool.GetComponent<Camera>(root, "CameraAnchor/Camera");
            if (camera != null)
            {
                cameraData = camera.GetUniversalAdditionalCameraData();
            }

            imageBgRoot = Tool.GetComponent<Transform>(root, "BgRoot/ImageBgRoot");
            bg_Image = Tool.GetComponent<RawImage>(root, "BgRoot/ImageBgRoot/Image");
            prefabRoot = Tool.GetComponent<Transform>(root, "BgRoot/PrefabRoot");
            main = Tool.GetComponent<Transform>(root, "Main");
            forword = Tool.GetComponent<Transform>(root, "Forword");
            cameraController = new CameraController(camera, cameraAnchor);
        }

        public void SetMainStage()
        {
            this.camera.tag = "MainCamera";
        }

        public void ShowGameObject(GameObject obj)
        {
            SetModelVisible(true);
            Tool.ClearChild(main);
            obj.transform.SetParent(main);
            obj.transform.localPosition = Vector3.zero;
        }

        public void Show(Sphere sphere)
        {
            ShowGameObject(sphere.Root);
        }

        public void SetStageVisible(bool visible)
        {
            root.gameObject.SetActive(visible);
            cameraController.SetVisible(visible);
        }

        public void SetBgVisible(bool visible)
        {
            this.prefabRoot.gameObject.SetActive(visible);
            imageBgRoot.gameObject.SetActive(visible);
        }

        public void SetModelVisible(bool visible)
        {
            this.main.gameObject.SetActive(visible);
        }

        public void SetForWordVisible(bool visible)
        {
            this.forword.gameObject.SetActive(visible);
        }
        public void Destory()
        {
            SetStageVisible(false);
            GameObject.Destroy(root);
        }

        #region 背景

        public void Decorate(string id)
        {
            var config = Backgrounds_Config.Inst.Backgrounds;
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

        #endregion

        #region 相机

        public void SetCameraPostion(double[] postion)
        {
            cameraController.SetCameraPostion(postion);
        }

        public void SetCameraRotation(double[] rotation)
        {
            cameraController.SetCameraRotation(rotation);
        }

        public void SetCamera(string id)
        {
            cameraController.UseConfig(id);
        }

        public void AddOverLayCamera(Camera camera)
        {
            if (cameraData.renderType != CameraRenderType.Base)
            {
                Debug.LogError($"camere:{this.camera.name} render Type is not base");
                return;
            }

            if (cameraData.cameraStack == null)
            {
                Debuger.LogError($"camere:{this.camera.name} camera stack is null");
                return;
            }

            if (cameraData.cameraStack.Contains(camera))
            {
                return;
            }

            cameraData.cameraStack.Add(camera);
        }

        public void RemoveOverLayCamera(Camera camera)
        {
            if (cameraData.renderType != CameraRenderType.Base)
            {
                Debug.LogError($"camere:{this.camera.name} render Type is not base");
                return;
            }

            if (cameraData.cameraStack == null)
            {
                Debuger.LogError($"camere:{this.camera.name} camera stack is null");
                return;
            }

            if (!cameraData.cameraStack.Contains(camera))
            {
                return;
            }

            cameraData.cameraStack.Remove(camera);
        }

        public void SetCameraStack(List<Camera> cacheOverLayCameras)
        {
            if (cameraData.renderType != CameraRenderType.Base)
            {
                Debug.LogError($"camere:{this.camera.name} render Type is not base");
                return;
            }

            if (cameraData.cameraStack == null)
            {
                Debuger.LogError($"camere:{this.camera.name} camera stack is null");
                return;
            }

            cameraData.cameraStack.Clear();
            cameraData.cameraStack.AddRange(cacheOverLayCameras);
        }

        #endregion

        public void ReSet()
        {
            Tool.ClearChild(main);
            Tool.ClearChild(prefabRoot);
            bg_Image.texture = null;
            prefabRoot.gameObject.SetActive(false);
            this.imageBgRoot.gameObject.SetActive(false);
            cameraData.cameraStack?.Clear();
            SetStageVisible(false);
        }
    }
}