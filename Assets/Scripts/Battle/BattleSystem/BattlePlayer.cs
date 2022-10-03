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

    public void SetActions(List<BattleAction> actions)
    {
        this.actions = new List<BattleAction>(actions);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void ChooseAction()
    {
        List<BattleAction> currentActions = new List<BattleAction>(actions);
        foreach (Status status in statuses)
        {
            currentActions = status.ModifyBattleActions(currentActions);
        }
        BattleUI.instance.PromptAction(currentActions);
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
        int finalMpDrain = Mathf.Min(currentMp, mp);
        currentMp -= finalMpDrain;
        if (finalMpDrain != 0)
        {
            BattleController.instance.AddDamageRecord(new DamageAnimationRecord(this, finalMpDrain, DamageAnimationRecord.DamageType.MP));
        }
    }

    public override bool CanUseAction(BattleAction action)
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
        int finalHealAmount = Mathf.Min(mp, maxMp - currentMp);
        currentMp += finalHealAmount;
        if (finalHealAmount != 0)
        {
            BattleController.instance.AddDamageRecord(new DamageAnimationRecord(this, -finalHealAmount, DamageAnimationRecord.DamageType.MP));
        }
    }
}
