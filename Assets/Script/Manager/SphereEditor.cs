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
        public Action onStartEditor;
        public Action onEndEditor;
        private Coroutine cor;
        private bool enable;
        private Vector3 startPos;
        private BuildingInSphere building;
        private Sphere sphere;
        private bool hode;
        public SphereEditor(Sphere sphere)
        {
            this.sphere = sphere;
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
                if (building!=null)
                {
                    Ray ray = StageManager.Inst.ShowStage.Camera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit, 1000f, 1<<9))
                    {
                        if (rayHit.transform==building.root.transform)
                        {
                            hode = true;
                        }
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (building==null)
                {
                    var dis = Vector3.Distance(startPos, Input.mousePosition);
                    if (dis > 0.1f)
                    {
                        return;
                    }
                    Ray ray = StageManager.Inst.ShowStage.Camera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit, 1000f, 1<<9))
                    {
                        if (rayHit.transform)
                        {
                            building = sphere.GetBuilding(rayHit.transform.gameObject);
                            StartEditor();
                        }
                    }
                    else
                    {
                        //没有点击到碰撞物
                    }
                }
                else
                {
                    var dis = Vector3.Distance(startPos, Input.mousePosition);
                    if (dis <= 0.1f)
                    {
                        //没有移动
                        return;
                    }
                    else
                    {
                        if (hode)
                        {
                            building.Resume();
                            // EndEditor();
                        }
                        else
                        {

                        }
                    }
                }
                hode = false;
            }

            if (Input.GetMouseButton(0))
            {
                if (hode)
                {
                    building.Pause();
                    Ray ray = StageManager.Inst.ShowStage.Camera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit, 1000f, 1 << 8))
                    {
                        building.root.transform.position = rayHit.point;
                    }
                }
            }
        }

        private void StartEditor()
        {
            sphere.SetCellVisible(true);
            if (building == null)
            {
                return;
            }

            building.PlayAni("buildingselecttimeline", true);
            onStartEditor?.Invoke();
        }

        public void EndEditor()
        {
            sphere.SetCellVisible(false);
            if (building == null)
            {
                return;
            }
            building.Stop();
            building.root.transform.position = SphereMap.GetPositionByGrid(building.grid);
            building = null;
            hode = false;
            onEndEditor?.Invoke();
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