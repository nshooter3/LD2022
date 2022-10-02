using UnityEngine;

/// <summary>
/// An animation that ends immediately.
/// Used for controlling instant effects within the animation queue.
/// </summary>
public abstract class InstantAnimation : BattleAnimation
{
    protected override void OnAnimationStart()
    {
    }

    public override void UpdateAnimation()
    {
    }

    public override bool IsAnimationFinished()
    {
        return true;
    }
}
