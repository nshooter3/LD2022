using UnityEngine;

public class AddInterferenceAnimation : InstantAnimation
{
    public BattleUIInterference interference;

    protected override void OnAnimationEnd()
    {
        BattleUI.instance.AddInterference(interference);
        Destroy(this);
    }
}
