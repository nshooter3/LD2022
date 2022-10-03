using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DurationStatus : Status
{
    [SerializeField]
    private int statusDuration;
    [SerializeField]
    private bool displayDuration;
    public bool DisplayDuration { get { return displayDuration; } }

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
