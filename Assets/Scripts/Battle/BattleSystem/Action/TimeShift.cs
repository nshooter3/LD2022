using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShift : DurationStatus
{
    public override void OnStatusAdded()
    {
        var newTime = MoveTimer.instance.CurrentTimerDuration - 1;
        MoveTimer.instance.SetNewTimer(newTime);
        base.OnStatusAdded();
    }

    public override void OnStatusRepeat()
    {
        var newTime = MoveTimer.instance.CurrentTimerDuration - 1;
        MoveTimer.instance.SetNewTimer(newTime);
    }

    public override void OnStatusRemove()
    {
        MoveTimer.instance.ResetTimerToDefault();
    }
}
