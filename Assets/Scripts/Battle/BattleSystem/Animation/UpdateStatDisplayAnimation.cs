using UnityEngine;

public class UpdateStatDisplayAnimation : InstantAnimation
{
    protected override void OnAnimationEnd()
    {
        BattleUI.instance.UpdateStatBars();
        Destroy(this);
    }
}
