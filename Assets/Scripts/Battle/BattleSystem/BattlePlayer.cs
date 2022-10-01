using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : BattleParticipant
{
    public int currentMp { get; private set; }
    [SerializeField]
    private int maxMp;
    public int MaxMp { get { return maxMp; } }
    [SerializeField]
    private int mpRestoreRate;
    public List<BattleParticipant> targets { get; private set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        currentMp = maxMp;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void ChooseAction()
    {
        BattleUI.instance.PromptAction(actions);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void ChoosePlayerAction(BattleAction action, List<BattleParticipant> targets)
    {
        currentAction = action;
        this.targets = targets;
        BattleController.instance.RunBattleTurn();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void DrainMp(int mp)
    {
        currentMp = Mathf.Max(0, currentMp - mp);
    }

    public bool CanUseAction(BattleAction action)
    {
        return currentMp >= action.MpCost;
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        RestoreMp(mpRestoreRate);
    }

    public void RestoreMp(int mp)
    {
        currentMp = Mathf.Min(maxMp, currentMp + mp);
    }
}
