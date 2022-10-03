using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBehaviorCounter : SequenceCombatBehavior
{
    public Dictionary<BattleAction, int> actionCounter = new Dictionary<BattleAction, int>();
    public List<ActionLimiter> actionLimiter = new List<ActionLimiter>();
    public override BattleAction ChooseAction(List<BattleAction> actions)
    {
        BattleAction toReturn = actions[actionIndexer];
        foreach (var entry in actionLimiter)
        {
            if (entry.battleAction==toReturn)
            {
                if (actionCounter.ContainsKey(toReturn))
                {
                    if (entry.LimitUses <= actionCounter[toReturn])
                    {
                        if (actionIndexer + 1 >= actions.Count)
                        {
                            actionIndexer = 0;
                        }
                        toReturn = actions[actionIndexer];
                        break;
                    }
                }
                if (actionCounter.ContainsKey(toReturn))
                {
                    actionCounter[toReturn]++;
                }
                else
                {
                    actionCounter.Add(toReturn, 1);
                }
            }
        }

        NextAction(actions);
        return toReturn;
    }
}
