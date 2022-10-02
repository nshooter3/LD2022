using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DurationStatus : Status
{
    [SerializeField]
    private int statusDuration;

    protected int turnsRemaining;

    public override void OnStatusAdded()
    {
        turnsRemaining = statusDuration;
    }

    public override void OnTurnEnd()
    {
        if (--turnsRemaining <= 0)
        {
            RemoveStatus();
        }
    }
}
