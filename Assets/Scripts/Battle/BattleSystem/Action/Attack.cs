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
    /// Element type of attack to apply.
    /// </summary>
    [SerializeField]
    private ElementTypes.ElementType elementType = ElementTypes.ElementType.Typeless;

    public override void RunAction(BattleParticipant user, BattleParticipant enemy)
    {
        enemy.DealDamage(damage, elementType);

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
