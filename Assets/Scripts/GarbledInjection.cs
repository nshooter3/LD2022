using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbledInjection : BattleAction
{
    public BattleAction injectedAction;
    private List<BattleAction> temporaryActionList;
    public override string GetIntentDisplay()
    {
        return "Debuff";
    }

    protected override void OnRunAction(BattleParticipant user, BattleParticipant target)
    {
        temporaryActionList = target.Actions;
        temporaryActionList.Add(injectedAction);
        temporaryActionList.Add(injectedAction);
        if (target is BattlePlayer)
        {
            (target as BattlePlayer).SetActions(temporaryActionList);
        }
    }
}
