using UnityEngine;

public class DisplayActionAnimation : InstantAnimation
{
    protected override void OnAnimationEnd()
    {
        BattleUI.instance.DisplayActionPrompt();
        Destroy(this);
    }
}
