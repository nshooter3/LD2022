using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionCombination : BattleAction
{
    private List<BattleAction> childActions = new List<BattleAction>();

    protected override void Start()
    {
        FindChildren();
        base.Start();
    }

    public override string GetIntentDisplay()
    {
        foreach (BattleAction childAction in childActions)
        {
            string intent = childAction.GetIntentDisplay();
            if (intent != "")
            {
                return intent;
            }
        }
        return "";
    }

    public override BattleAction InstantiateAction(BattleParticipant user)
    {
        ActionCombination newAction = (ActionCombination) base.InstantiateAction(user);
        foreach (BattleAction childAction in childActions)
        {
            if (childAction != this)
            {
                Instantiate(childAction, newAction.transform);
            }
        }
        newAction.FindChildren();
        return newAction;
    }

    protected override void OnRunAction(BattleParticipant user, BattleParticipant target)
    {
        childActions.ForEach(action => action.RunAction(user, target));
    }

    private void FindChildren()
    {
        childActions.Clear();
        foreach (BattleAction childAction in GetComponents<BattleAction>())
        {
            if (childAction != this)
            {
                childActions.Add(childAction);
            }
        }
    }
}
