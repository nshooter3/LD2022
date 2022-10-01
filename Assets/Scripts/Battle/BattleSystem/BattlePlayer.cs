using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : BattleParticipant
{
    private int currentMp;
    [SerializeField]
    private int maxMp;
    [SerializeField]
    private List<BattleAction> actions;
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
        BattleUI.instance.PromptAction(actions, BattleController.instance.aliveEnemies);
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
}
