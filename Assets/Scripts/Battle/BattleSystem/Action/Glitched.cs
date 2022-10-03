using UnityEngine;

public class Glitched : BattleAction
{
    /// <summary>
    /// The Base Damage the attack does.
    /// </summary>
    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }

    /// <summary>
    /// Element type of attack to apply.
    /// </summary>
    [SerializeField]
    private ElementType elementType = ElementType.Typeless;
    public ElementType ElementType { get { return elementType; } }

    protected override void OnRunAction(BattleParticipant user, BattleParticipant target)
    {
        int dealtDamage = target.DealDamage(damage, elementType);
        OnDealDamage(user, target, dealtDamage);

        if (recoil > 0)
        {
            user.DealDamage(recoil, ElementType.Typeless);
        }
    }

    public override IntentType GetIntentType()
    {
        return IntentType.ATTACK;
    }

    public override string GetIntentText()
    {
        return damage.ToString();
    }

    public virtual void OnDealDamage(BattleParticipant user, BattleParticipant target, int damage)
    {
    }
}
