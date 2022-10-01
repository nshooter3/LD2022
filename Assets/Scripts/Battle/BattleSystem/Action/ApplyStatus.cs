using UnityEngine;

public class ApplyStatus : BattleAction
{
    [SerializeField]
    private Status status;

    protected override void OnRunAction(BattleParticipant user, BattleParticipant target)
    {
        target.AddStatus(status);
    }

    public override string GetIntentDisplay()
    {
        return "";
    }
}
