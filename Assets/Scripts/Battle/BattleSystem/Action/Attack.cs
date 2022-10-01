using UnityEngine;

public class Attack : BattleAction
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private int recoil;
    [SerializeField]
    private int mpCost;

    public override void RunAction(BattleParticipant user, BattleParticipant enemy)
    {
        enemy.DealDamage(damage);
        if (recoil > 0)
        {
            user.DealDamage(recoil);
        }
        if (mpCost > 0)
        {
            user.DrainMp(mpCost);
        }
    }
}
