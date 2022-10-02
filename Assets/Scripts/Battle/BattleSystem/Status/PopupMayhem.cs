using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupMayhem : DurationStatus
{
    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        this.TickTurnDuration();
        if (TurnsRemaining == 0)
        {
            this.RemoveStatus();
        }
    }

    public override void OnStatusRemove()
    {
        PopUpGenerator.instance.ToggleSpawnPopups(false);
    }

    public override void OnStatusAdded()
    {
        base.OnStatusAdded();
        PopUpGenerator.instance.ToggleSpawnPopups(true);
    }
}
