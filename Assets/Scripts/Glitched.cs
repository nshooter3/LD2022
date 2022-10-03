using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitched : BattleAction
{
    public override string GetIntentDisplay()
    {
        return "Glitched";
    }

    protected override void OnRunAction(BattleParticipant user, BattleParticipant target)
    {
    }
}
