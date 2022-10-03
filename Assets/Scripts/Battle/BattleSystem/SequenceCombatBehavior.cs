using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceCombatBehavior : CombatBehavior
{
    /// <summary>
    /// The current action indexer.
    /// </summary>
    protected int actionIndexer = 0;

    public override BattleAction ChooseAction(List<BattleAction> actions)
    {
        BattleAction toReturn = actions[actionIndexer];
        NextAction(actions);
        return toReturn;
    }

    /// <summary>
    /// Increments the counter for the AI Behavior.
    /// </summary>
    public void NextAction(List<BattleAction> actions)
    {
        ++actionIndexer;
        if (actionIndexer >= actions.Count)
        {
            actionIndexer = 0;
        }
    }
}
