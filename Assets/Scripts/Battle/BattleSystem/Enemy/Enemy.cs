using UnityEngine;

public class Enemy : BattleParticipant
{
    /// <summary>
    /// The Current Combat Behavior for this Enemy.
    /// </summary>
    [SerializeField]
    private CombatBehavior combatBehavior;

    public override void Initialize()
    {
        base.Initialize();
        combatBehavior = Instantiate(combatBehavior, transform);
    }

    /// <summary>
    /// Use the current action.
    /// </summary>
    public override void ChooseAction()
    {
        currentAction = combatBehavior.ChooseAction(actions);
    }

    public override void DrainMp(int mp)
    {
    }
}
