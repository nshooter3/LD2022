using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbledInjection : Status
{
    public BattleAction injectedAction;
    public int counter = 0;
    public override List<BattleAction> ModifyBattleActions(List<BattleAction> actions)
    {

        for (int i = 0; i < counter; ++i)
        {
            actions.Add(injectedAction);
            actions.Add(injectedAction);
        }
        return actions;
    }

    public override void OnStatusAdded()
    {
        ++counter;
        base.OnStatusAdded();
    }

    public override void OnStatusRepeat()
    {
        ++counter;
    }

}
