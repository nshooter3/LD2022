using UnityEngine;

public class Enemy : BattleParticipant
{
    /// <summary>
    /// The Current Combat Behavior for this Enemy.
    /// </summary>
    [SerializeField]
    private SequenceCombatBehavior combatBehavior;

    /// <summary>
    /// Use the current action.
    /// </summary>
    public override void ChooseAction()
    {
        currentAction = combatBehavior.ChooseAction();
        combatBehavior.NextAction();
    }

    public override void DrainMp(int mp)
    {
    }
}
