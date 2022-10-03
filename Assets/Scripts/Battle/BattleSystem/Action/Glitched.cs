using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitched : BattleAction
{

    public override IntentType GetIntentType()
    {
        return IntentType.NONE;
    }

    protected override void OnRunAction(BattleParticipant user, BattleParticipant target)
    {
    }
}
