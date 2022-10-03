using UnityEngine;

public class Combustion : DurationStatus
{
    [SerializeField]
    private int damagePerTurn;

    public override void OnTurnEnd()
    {
        participant.DealDamage(damagePerTurn, ElementType.Typeless);
        base.OnTurnEnd();
    }
}
