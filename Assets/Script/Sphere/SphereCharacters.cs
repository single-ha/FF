using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script
{
    public class SphereCharacters:SphereComponent
    {
        public List<Character> characters;
        public SphereCharacters(Sphere sphere) : base(sphere)
        {
            characters = new List<Character>();
        }
        public void AddCharacter(Character character)
        {
            character.onLoaded = delegate()
            {
                OnShow(character);
            };
            characters.Add(character);
        }

        private void OnShow(Character character)
        {
            character.SetParent(this.root.transform);
            character.root.transform.localPosition = Vector3.zero;
            character.root.transform.localScale = Vector3.one;
            Vector3 grid = sphere.sphereMap.SampleRandomPostion();
            var pos = SphereMap.GetPositionByGrid(grid);
            pos = sphere.GetWordPos(pos);
            character.Warp(pos);
            character.SetAI(true);
        }
    }
}