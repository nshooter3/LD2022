using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanishStatus : DurationStatus
{
    public override void OnStatusAdded()
    {
        BattleUI.instance.SetActionsSpanishName();
        base.OnStatusAdded();
    }

    public override void OnStatusRemove()
    {
        base.OnStatusRemove();
        BattleUI.instance.SetActionsDefaultName();
    }
}
