using UnityEngine;

public abstract class Status : MonoBehaviour
{
    private BattleParticipant participant;

    public virtual int ModifyIncomingDamage(int damage)
    {
        return damage;
    }

    public virtual void OnTurnEnd()
    {
    }

    public void AddStatus(BattleParticipant participant)
    {
        this.participant = participant;
    }

    public void RemoveStatus()
    {
        participant.RemoveStatus(this);
    }
}