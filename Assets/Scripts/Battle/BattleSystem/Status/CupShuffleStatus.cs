using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CupShuffleStatus : InterferenceStatus
{
    [SerializeField]
    private int numActions;

    public override List<BattleAction> ModifyBattleActions(List<BattleAction> actions)
    {
        return RandomUtil.ChooseRandomElementsFromList(actions.FindAll(action => participant.CanUseAction(action)), numActions);
    }
}
