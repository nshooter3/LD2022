using System;
using UnityEngine;

public abstract class Status : MonoBehaviour, IEquatable<Status>
{
    private BattleParticipant participant;

    [SerializeField]
    public Sprite statusSprite;

    public virtual int ModifyIncomingDamage(int damage)
    {
        return damage;
    }


    public virtual void OnStatusAdded()
    {
    }

    public virtual void OnTurnEnd()
    {
    }

    public void AddStatus(BattleParticipant participant)
    {
        this.participant = participant;
        OnStatusAdded();
    }

    public void RemoveStatus()
    {
        OnStatusRemove();
        participant.RemoveStatus(this);
    }

    public virtual void OnStatusRemove()
    {
    }

    public bool Equals(Status otherStatus)
    {
        return GetType() == otherStatus.GetType();
    }
}
