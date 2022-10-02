using System;
using UnityEngine;

public class RemoveInterferenceAnimation : InstantAnimation
{
    public Type interference;

    protected override void OnAnimationEnd()
    {
        BattleUI.instance.RemoveInterference(interference);
        Destroy(this);
    }
}
