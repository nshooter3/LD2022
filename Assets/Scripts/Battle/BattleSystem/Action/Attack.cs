using UnityEngine;

public class Attack : BattleAction
{
    /// <summary>
    /// The Base Damage the attack does.
    /// </summary>
    [SerializeField]
    private int damage;

    /// <summary>
    /// Element type of attack to apply.
    /// </summary>
    [SerializeField]
    private ElementType elementType = ElementType.Typeless;

    protected override void OnRunAction(BattleParticipant user, BattleParticipant target)
    {
        target.DealDamage(damage, elementType);

        if (recoil > 0)
        {
            user.DealRecoilDamage(recoil);
        }
    }

    public override string GetIntentDisplay()
    {
        return "Attack";
    }
}
