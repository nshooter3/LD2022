using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An animation tied to a battle action.
/// </summary>
public abstract class ActionAnimation : BattleAnimation
{
    [HideInInspector]
    public BattleAction action;

    public override void StartAnimation(BattleParticipantDisplay userDisplay, List<BattleParticipantDisplay> targetDisplays)
    {
        gameObject.SetActive(true);
        base.StartAnimation(userDisplay, targetDisplays);
    }

    public override void EndAnimation()
    {
        base.EndAnimation();
        Destroy(gameObject);
    }
}
