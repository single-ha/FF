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
            NavMeshHit hit;
            var pos = SphereMap.GetPositionByGrid(grid);
            if (NavMesh.SamplePosition(pos, out hit,10f,NavMesh.AllAreas))
            {
                character.Warp(hit.position);
            }
            else
            {
                Debuger.LogWarning("未找到代理可移动得点");
            }
            character.SetAI(true);
            characters.Add(character);
        }
    }
}