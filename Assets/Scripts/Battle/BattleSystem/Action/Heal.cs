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

    /// <summary>
    /// The cost of MP per Attack.
    /// </summary>
    [SerializeField]
    private int mpCost;

    public override string GetIntentDisplay()
    {
        return "Heal " + healAmount;
    }

    public override void RunAction(BattleParticipant user, BattleParticipant enemy)
    {
        user.Heal(healAmount);
        if (mpCost > 0)
        {
            user.DrainMp(mpCost);
        }
    }
}
