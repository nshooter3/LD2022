using UnityEngine;

public class Guard : Status
{

    public override int ModifyIncomingDamage(int damage)
    {
        return damage / 2;
    }

    public override void OnTurnEnd()
    {
        RemoveStatus();
    }
}
