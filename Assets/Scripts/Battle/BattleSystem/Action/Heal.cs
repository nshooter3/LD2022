using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : BattleAction
{
    /// <summary>
    /// The amount the heal spell does.
    /// </summary>
    [SerializeField]
    private int healAmount;
    public int HealAmount { get { return healAmount; } }

    public override IntentType GetIntentType()
    {
        return IntentType.BUFF;
    }

    protected override void OnRunAction(BattleParticipant user, BattleParticipant target)
    {
        user.Heal(healAmount);
    }
}
