using UnityEngine;

public struct DamageAnimationRecord
{
    public BattleParticipant participant;
    public int damage;
    public DamageType damageType;

    public enum DamageType
    {
        HP,
        MP
    }

    public DamageAnimationRecord(BattleParticipant participant, int damage, DamageType damageType)
    {
        this.participant = participant;
        this.damage = damage;
        this.damageType = damageType;
    }
}
