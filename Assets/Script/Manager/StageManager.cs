﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Script.Manager
{
    public class StageManager : Instance<StageManager>
    {
        private Stage mainStage;
        private Stage showStage;
        private List<Camera> cacheOverLayCameras = new List<Camera>();

        public Stage MainStage
        {
            get { return mainStage; }
        }

        public void Init()
        {
            mainStage = NewStage();
            mainStage.SetMainStage();
            ShowStage();
            MainStage.SetModelVisible(false);
        }

        public void AddOverLayCamera(Camera camera)
        {
            if (camera.GetUniversalAdditionalCameraData().renderType == CameraRenderType.Overlay)
            {
                showStage?.AddOverLayCamera(camera);
                if (!cacheOverLayCameras.Contains(camera))
                {
                    cacheOverLayCameras.Add(camera);
                }
            }
            else
            {
                Debuger.LogError($"camera:{camera.name} render type is not overlay");
            }
        }

        public void RemoveOverLayCamera(Camera camera)
        {
            if (camera.GetUniversalAdditionalCameraData().renderType == CameraRenderType.Overlay)
            {
                showStage.RemoveOverLayCamera(camera);
                if (cacheOverLayCameras.Contains(camera))
                {
                    cacheOverLayCameras.Remove(camera);
                }
            }
            else
            {
                Debuger.LogError($"camera:{camera.name} render type is not overlay");
            }
        }

        public void ShowStage(Stage stage = null)
        {
            if (stage == null)
            {
                stage = MainStage;
            }

            if (stage != showStage)
            {
                if (showStage != null)
                {
                    showStage.SetStageVisible(false);
                }
                showStage = stage;
                showStage.SetStageVisible(true);
                SetCameraStack();
            }
        }

        public void SetCameraStack()
        {
            if (showStage != null)
            {
                showStage.SetCameraStack(cacheOverLayCameras);
            }
        }

        public Stage NewStage()
        {
           var stage= ObjectPoolManager.Inst.GetObject<Stage>();
           stage.SetStageVisible(false);
           return stage;
        }

        public void RecycleStage(Stage stage)
        {
            ObjectPoolManager.Inst.Recycle(stage);
        }
    }
}