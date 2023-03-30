using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script
{
    public class SphereCharacters:SphereComponent
    {
        public List<Character> characters;
        public SphereCharacters(Transform root) : base(root)
        {
            characters = new List<Character>();
        }
        public void AddCharacter(Character character,Vector2 grid)
        {
            character.SetParent(this.root.gameObject);
            var pos = SphereMap.GetPositionByGrid(grid);
            character.Warp(pos);
            character.SetAI(true);
            characters.Add(character);
        }
    }
}