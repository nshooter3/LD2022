using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceCombatBehavior : CombatBehavior
{

    /// <summary>
    /// The current action indexer.
    /// </summary>
    public int actionIndexer = 0;

    public override BattleAction ChooseAction()
    {
        if (actions == null)
        {
            Debug.LogError("Actions have not been populated.");
        }
        return actions[actionIndexer];
    }

    /// <summary>
    /// Increments the counter for the AI Behavior.
    /// </summary>
    public void NextAction()
    {
        ++actionIndexer;
        if (actionIndexer >= actions.Count)
        {
            actionIndexer = 0;
        }
    }
}
