using UnityEngine;

public abstract class BattleAction : MonoBehaviour
{
    [SerializeField]
    private string actionName;
    public string ActionName { get { return actionName; } }

    /// <summary>
    /// The cost of MP per Attack.
    /// </summary>
    [SerializeField]
    protected int mpCost;
    public int MpCost { get { return mpCost; } }

    [SerializeField]
    private bool targetSelf;
    public bool TargetSelf { get { return targetSelf; } }

    [SerializeField]
    private bool areaOfEffect;
    public bool AreaOfEffect { get { return areaOfEffect; } }

    public void RunAction(BattleParticipant user, BattleParticipant target)
    {
        OnRunAction(user, target);
        if (mpCost > 0)
        {
            user.DrainMp(mpCost);
        }
    }

    protected abstract void OnRunAction(BattleParticipant user, BattleParticipant target);

    public abstract string GetIntentDisplay();
}
