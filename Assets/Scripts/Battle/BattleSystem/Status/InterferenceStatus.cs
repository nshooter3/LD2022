using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterferenceStatus : DurationStatus
{
    [SerializeField]
    private BattleUIInterference interference;

    public override void OnStatusAdded()
    {
        AddInterferenceAnimation animation = BattleController.instance.gameObject.AddComponent<AddInterferenceAnimation>();
        animation.interference = Instantiate<BattleUIInterference>(interference);
        BattleController.instance.QueueAnimation(animation);
        base.OnStatusAdded();
    }

    public override void OnStatusRemove()
    {
        RemoveInterferenceAnimation animation = BattleController.instance.gameObject.AddComponent<RemoveInterferenceAnimation>();
        animation.interference = interference.GetType();
        BattleController.instance.QueueAnimation(animation);
    }
}
