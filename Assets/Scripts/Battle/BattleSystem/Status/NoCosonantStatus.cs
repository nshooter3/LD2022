using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCosonantStatus : DurationStatus
{
    public override void OnStatusAdded()
    {
        base.OnStatusAdded();
        BattleUI.instance.SetActionsNoConsonantsName();
    }

    public override void OnStatusRemove()
    {
        base.OnStatusRemove();
        BattleUI.instance.SetActionsDefaultName();
    }
}
