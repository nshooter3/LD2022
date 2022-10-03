using UnityEngine;

public struct DamageAnimationRecord
{
    public BattleParticipant participant;
    public int damage;
    public DamageType damageType;
    public TypeEffectiveness typeEffectiveness;

    public enum DamageType
    {
        HP,
        MP
    }

    public enum TypeEffectiveness
    {
        Neutral,
        Weak,
        Resist
    }

    public DamageAnimationRecord(BattleParticipant participant, int damage, DamageType damageType)
    {
        this.participant = participant;
        this.damage = damage;
        this.damageType = damageType;
        typeEffectiveness = TypeEffectiveness.Neutral;
    }

    public DamageAnimationRecord(BattleParticipant participant, int damage, DamageType damageType, TypeEffectiveness typeEffectiveness)
    {
        this.participant = participant;
        this.damage = damage;
        this.damageType = damageType;
        this.typeEffectiveness = typeEffectiveness;
    }
}
