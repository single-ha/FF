using System;
using System.Collections;
using Assets.Script;
using Lean.Touch;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.Manager
{
    public class SphereEditor
    {
        private event Action<GameObject, Vector3> ClickEvent;
        private event Action<Vector3> HandleEvent;
        private Coroutine cor;
        private int layerMask;
        private bool enable;
        private Vector3 startPos;
        public SphereEditor()
        {
            this.layerMask = 1<<8;
        }

        public bool Enable
        {
            get
            {
                return enable;
            }
            set
            {
                enable = value;
                if (enable)
                {
                    OnEnable();
                }
                else
                {
                    OnDisable();
                }
            }
        }
        private void OnEnable()
        {
            OnDisable();
            cor = GameMain.Inst.StartCoroutine(Update());
        }

        private IEnumerator Update()
        {
            while (enable)
            {
                Click();
                yield return null;
            }
        }

        private void Click()
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                var dis = Vector3.Distance(startPos, Input.mousePosition);
                if (dis>0.1f)
                {
                    return;
                }
                Ray ray = StageManager.Inst.ShowStage.Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit, 1000f, layerMask))
                {
                    if (rayHit.transform)
                    {
                        ClickEvent?.Invoke(rayHit.transform.gameObject, rayHit.point);
                    }
                }
                else
                {
                    //没有点击到碰撞物
                }
            }

            if (Input.GetMouseButton(0))
            {
                HandleEvent?.Invoke(Input.mousePosition);
            }
        }

        public void SetClick(Action<GameObject, Vector3> onClick)
        {
            this.ClickEvent = onClick;
        }
        public void RegisterClick(Action<GameObject, Vector3> onClick)
        {
            this.ClickEvent += onClick;
        }

        public void RemoveClick(Action<GameObject, Vector3> OnClick)
        {
            ClickEvent -= OnClick;
        }

        public void SetLayerMask(int layerMask)
        {
            this.layerMask = layerMask;
        }
        public void AddLayMask(int layer)
        {
            layerMask |= 1 << layer;
        }
        public void RemoveMask(int layer)
        {
            layerMask ^= 1 << layer;
        }

        public void AddLayMask(string layerName)
        {
            int layer = LayerMask.NameToLayer(layerName);
            AddLayMask(layer);
        }

        public void RemoveMask(string layerName)
        {
            int layer = LayerMask.NameToLayer(layerName);
            RemoveMask(layer);
        }

        private void OnDisable()
        {
            if (cor != null)
            {
                GameMain.Inst.StopCoroutine(cor);
                cor = null;
            }
        }
    }
}