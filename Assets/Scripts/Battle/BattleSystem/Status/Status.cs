using System;
using UnityEngine;

public abstract class Status : MonoBehaviour, IEquatable<Status>
{
    private int statusIdentifier;
    private BattleParticipant participant;

    [SerializeField]
    public Sprite statusSprite;

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

    public bool Equals(Status otherStatus)
    {
        if (this.statusIdentifier == otherStatus.statusIdentifier)
        {
            return true;
        }
        return false;
    }
}
