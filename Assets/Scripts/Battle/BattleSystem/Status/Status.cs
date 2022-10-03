using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status : MonoBehaviour, IEquatable<Status>
{
    protected BattleParticipant participant;

    [SerializeField]
    public List<Sprite> statusSprites;

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

    public virtual List<BattleAction> ModifyBattleActions(List<BattleAction> actions)
    {
        return actions;
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
