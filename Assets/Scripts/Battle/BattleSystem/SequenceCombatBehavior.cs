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
        if (Actions == null)
        {
            Debug.LogError("Actions have not been populated.");
        }
        return Actions[ActionIndexer];
    }

    /// <summary>
    /// Increments the counter for the AI Behavior.
    /// </summary>
    public void NextAction()
    {
        ++ActionIndexer;
        if (ActionIndexer >= Actions.Count)
        {
            ActionIndexer = 0;
        }
    }
}
