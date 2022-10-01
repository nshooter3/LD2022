using UnityEngine;

public class Attack : BattleAction
{
    /// <summary>
    /// The Base Damage the attack does.
    /// </summary>
    [SerializeField]
    private int damage;

    /// <summary>
    /// The Recoil Damage the attack does.
    /// </summary>
    [SerializeField]
    private int recoil;

    /// <summary>
    /// The cost of MP per Attack.
    /// </summary>
    [SerializeField]
    private int mpCost;

    /// <summary>
    /// Status to Apply. If no Status is applied, nothing happens.
    /// </summary>
    [SerializeField]
    private Status StatusToApply;

    /// <summary>
    /// Element type of attack to apply.
    /// </summary>
    [SerializeField]
    private ElementTypes.ElementType ElementType = ElementTypes.ElementType.Physical;


    /// <summary>
    /// The amount to heal the battle combatant.
    /// </summary>
    [SerializeField]
    private int HealAmount;

    public override void RunAction(BattleParticipant user, BattleParticipant enemy)
    {
        enemy.DealDamage(damage, ElementType);

        if (recoil > 0)
        {
            user.DealRecoilDamage(recoil);
        }

        if (mpCost > 0)
        {
            user.DrainMp(mpCost);
        }
    }
}
