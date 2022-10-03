using UnityEngine;

public class Guard : Status
{
    [SerializeField]
    private float damageMultiplier;

    public override int ModifyIncomingDamage(int damage)
    {
        return (int)(damage * damageMultiplier);
    }

    public override void OnTurnEnd()
    {
        RemoveStatus();
    }
}
