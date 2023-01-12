using System.IO.Pipes;
using Assets.Script;
using UnityEngine;

public class EdgeChecker:IChecker
{
    private int radius;

    public EdgeChecker(int radius)
    {
        this.radius = radius;
    }

    public bool Check(Vector2 grid,Vector2 size)
    {
        var pos = SphereMap.GetPositionByGrid(grid);
        pos = new Vector2(Mathf.Abs(pos.x), Mathf.Abs(pos.y));
        pos += size*SphereMap.SphereCell / 2;
        var s = Vector2.Distance(pos, Vector2.zero);
        return s <= radius;
    }
}