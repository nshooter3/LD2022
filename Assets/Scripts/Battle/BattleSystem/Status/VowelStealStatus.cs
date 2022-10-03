using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VowelStealStatus : DurationStatus
{
    public override void OnStatusAdded()
    {
        BattleUI.instance.SetActionsNoVowelsName();
        base.OnStatusAdded();
    }

    public override void OnStatusRemove()
    {
        base.OnStatusRemove();
        BattleUI.instance.SetActionsDefaultName();
    }
}
