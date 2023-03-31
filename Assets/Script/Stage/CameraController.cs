using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Data;
using Lean.Touch;
using UnityEngine;

namespace Assets.Script
{
    public class CameraController
    {
        private Transform cameraAnchor;
        private Camera camera;
        private CamerasConfig.CameraConfig config;

        private bool isControll=true;
        public bool IsControll
        {
            get => isControll;
            set => isControll = value;
        }
        private bool isRoting = false;
        private Vector2 fingerDelta=new Vector2(0,0);
        public float normalY;
        public bool CanMove_Y = true;
        public CameraController(Camera camera,Transform cameraAnchor)
        {
            this.cameraAnchor = cameraAnchor;
            this.camera = camera;
            SetVisible(true);
        }
        public void SetVisible(bool visible)
        {
            if (visible)
            {
                LeanTouch.OnGesture += OnGesture;
            }
            else
            {
                LeanTouch.OnGesture -= OnGesture;
            }
        }
        private void OnGesture(List<LeanFinger> fingers)
        {
            if (!isControll)
            {
                return;
            }
            
            if (config==null)
            {
                return;
            }
            bool isPointerOnUI = false;
            foreach (var finger in fingers)
            {
                if (finger.IsOverGui)
                {
                    isPointerOnUI = true;
                    break;
                }
            }
            if (isPointerOnUI)
            {
                return;
            }
            var pinchScale = LeanGesture.GetPinchScale();
            var fieldOfView = camera.fieldOfView;
            camera.fieldOfView = Mathf.Clamp(fieldOfView - fieldOfView * ((pinchScale - 1)), (float)config.min_fov, (float)config.max_fov);
            if (fingers != null && fingers.Count == 2 && Mathf.Abs((pinchScale - 1)) < 0.1f)
            {
                var finger = fingers[0];
                float adjustRatio = camera.fieldOfView / (float)config.fov;
                Vector3 inc = camera.transform.localPosition + new Vector3(-0.01f * finger.ScreenDelta.x * adjustRatio, -0.01f * finger.ScreenDelta.y * adjustRatio, 0f);
                camera.transform.localPosition = new Vector3(Mathf.Clamp(inc.x, -3.5f, 3.5f), Mathf.Clamp(inc.y, normalY - 4f, normalY + 4f), inc.z);
            }
            
            if (fingers.Count == 1)
            {
                var finger = fingers[0];

                fingerDelta += 0.27f*finger.ScreenDelta;
                if (!CanMove_Y)
                {
                    fingerDelta.y = 0;
                }
                if (!isRoting)
                {
                    GameMain.Inst.StartCoroutine(Co_RotSphere());
                }
            }
        }
        IEnumerator Co_RotSphere()
        {
            isRoting = true;
            while (Vector2.Distance(Vector2.zero, fingerDelta)>0.01f)
            {
                fingerDelta=Vector2.Lerp(fingerDelta, Vector2.zero, 0.4f);
                var rotate = cameraAnchor.eulerAngles;
                rotate.x -= fingerDelta.y;
                rotate.y += fingerDelta.x;
                rotate.x = Mathf.Clamp(rotate.x, 0, 40);
                cameraAnchor.eulerAngles = rotate;
                yield return null;
            }

            isRoting = false;
        }
        public void UseConfig(string id)
        {
            var cameraConfig = CamerasConfig.GetConfig(id);
            if (cameraConfig!=null)
            {
                this.config = cameraConfig;
                SetCameraPostion(this.config.position);
                SetCameraRotation(this.config.rotation);
                SetCameraFov(this.config.fov);
            }
        }
        private void SetCameraFov(double cFov)
        {
            this.camera.fieldOfView = (float)cFov;
        }

        public void SetCameraPostion(double[] postion)
        {
            this.camera.transform.localPosition = Tool.Array2V3(postion);
        }

        public void SetCameraRotation(double[] rotation)
        {
            this.cameraAnchor.transform.rotation = Quaternion.Euler(Tool.Array2V3(rotation));
        }
    }
}