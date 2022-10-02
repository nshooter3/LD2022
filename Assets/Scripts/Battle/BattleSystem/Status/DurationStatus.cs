using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DurationStatus : Status
{
    [SerializeField]
    private int statusDuration;

    private int turnsRemaining;

    public int TurnsRemaining
    {
        get 
        { 
            return turnsRemaining; 
        }
    }

    public override void OnStatusAdded()
    {
        turnsRemaining = statusDuration;
    }

    public void TickTurnDuration()
    {
        if (turnsRemaining > 0)
        {
            --turnsRemaining;
        }
    }

}