using UnityEngine;

public class Enemy : BattleParticipant
{
    /// <summary>
    /// The Current Combat Behavior for this Enemy.
    /// </summary>
    [SerializeField]
    private SequenceCombatBehavior CombatBehavior;

    /// <summary>
    /// Use the current action.
    /// </summary>
    public override void ChooseAction()
    {
        currentAction = CombatBehavior.ChooseAction();
        CombatBehavior.NextAction();
    }

    public override void DrainMp(int mp)
    {
    }
}
