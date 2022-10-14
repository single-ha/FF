﻿using Assets.Script.Config;
using UnityEngine;

namespace Assets.Script
{
    public class FloorHelp
    {
        private SphereFloor floor;
        private GameObject meshObj;

        public FloorHelp(SphereFloor floor )
        {
            this.floor = floor;
        }
        public void ShowMask()
        {
            if (meshObj==null)
            {
                CreatObj();
            }
            else
            {
                meshObj.gameObject.SetActive(true);
            }
        }

        private void CreatObj()
        {
            meshObj = new GameObject();
            meshObj.transform.SetParent(floor.FloorObj.transform.parent);
            meshObj.transform.localPosition = new Vector3(0, 0.1f, 0);
            Mesh mesh = CreadMesh();
            var mesh_component = meshObj.AddComponent<MeshFilter>();
            mesh_component.mesh = mesh;
            var mesh_render = meshObj.AddComponent<MeshRenderer>();

        }

        private Mesh CreadMesh()
        {
            var floor = this.floor.FloorObj.transform.Find("1");
            if (floor==null)
            {
                return null;
            }

            var meshFilter = floor.GetComponent<MeshFilter>();
            if (meshFilter==null)
            {
                return null;
            }

            for (int i = 0; i < meshFilter.mesh.uv.Length; i++)
            {
                Debug.Log(meshFilter.mesh.uv[i]);
            }
            return meshFilter.mesh;
        }

        private Vector3[] GetVertices()
        {
            return new Vector3[]
            {
                new Vector3(0,0,0),
                new Vector3(0,0,1),
                new Vector3(1,0,0),
            };
        }

        private int[] GetTriangles()
        {
            return new int[]
            {
                0,1,2
            };
        }
        public void HideMask()
        {
            if (meshObj!=null)
            {
                meshObj.gameObject.SetActive(false);
            }
        }
    }
}