using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceCombatBehavior : MonoBehaviour
{

    /// <summary>
    /// The current action indexer.
    /// </summary>
    public int ActionIndexer = 0;

    /// <summary>
    /// Number of Actions.
    /// </summary>
    public List<BattleAction> Actions = new List<BattleAction>();

    /// <summary>
    /// Chooses an action by incrementing the current attack index.
    /// </summary>
    /// <returns>An action chosen by current attack index.</returns>
    public virtual BattleAction ChooseAction()
    {
        if (Actions == null || Actions.Count == 0)
        {
            Debug.LogError("Actions have not been populated.");
        }
        return GetCurrentAction();
    }

    /// <summary>
    /// Increments the counter for the AI Behavior.
    /// </summary>
    public void NextTurn()
    {
        ++ActionIndexer;
        if (ActionIndexer >= Actions.Count)
        {
            ActionIndexer = 0;
        }
    }

    /// <summary>
    /// Gets the current action for the current indexer.
    /// </summary>
    /// <returns>The current battle action for the AI.</returns>
    public BattleAction GetCurrentAction()
    {
        return Actions[ActionIndexer];
    }
}
