using System.Collections.Generic;
using Assets.Script;
using Assets.Script.Manager;

public class ManagerList:Instance<ManagerList>
{
    public List<IManager> managers = new List<IManager>
    {
        ObjectPoolManager.Inst,
        ResManager.Inst,
        StageManager.Inst,
        UIManager.Inst,
    };
}