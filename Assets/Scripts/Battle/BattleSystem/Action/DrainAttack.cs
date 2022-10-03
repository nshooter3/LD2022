using UnityEngine;

public class DrainAttack : Attack
{
    public override void OnDealDamage(BattleParticipant user, BattleParticipant target, int damage)
    {
        user.Heal(damage);
    }
}
