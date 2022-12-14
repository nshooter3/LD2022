using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BattleParticipant : MonoBehaviour
{
    public int currentHp { get; private set; }
    [SerializeField]
    private int maxHp;
    public int MaxHp { get { return maxHp; } }

    [SerializeField]
    protected List<BattleAction> actions;

    public BattleAction currentAction { get; protected set; }

    [SerializeField]
    private ElementType currentElementType = ElementType.Typeless;
    /// <summary>
    /// The elemental type of the battle participant
    /// </summary>
    public ElementType CurrentElementType { get { return currentElementType; } }

    public bool Dead { get { return currentHp <= 0; } }

    /// <summary> Backing statuses list. Don't use this for most iteration. </summary>
    private List<Status> _statuses = new List<Status>();
    /// <summary> Use this statuses list for general iteration. It clones the list to avoid issues with statuses being removed during iteration. </summary>
    public List<Status> statuses { get { return new List<Status>(_statuses); } }

    public List<BattleAction> Actions { get { return actions; }}

    public struct TypeEffectivenessResult
    {
        public int damage;
        public DamageAnimationRecord.TypeEffectiveness typeEffectiveness;
    }

    public virtual void Initialize()
    {
        currentHp = maxHp;
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i] = actions[i].InstantiateAction(this);
        }
    }

    public abstract void ChooseAction();

    /// <summary>
    /// Handles when a Battle Participant attacks another battle participant.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="attackElement"></param>
    public int DealDamage(int damage, ElementType attackElement)
    {
        TypeEffectivenessResult result = CalculateElementalDamage(attackElement, damage);
        int finalDamage = result.damage;
        foreach (Status status in statuses)
        {
            finalDamage = status.ModifyIncomingDamage(finalDamage);
        }
        finalDamage = Mathf.Min(currentHp, finalDamage);
        currentHp -= finalDamage;
        if (finalDamage != 0)
        {
            BattleController.instance.AddDamageRecord(new DamageAnimationRecord(this, finalDamage, DamageAnimationRecord.DamageType.HP, result.typeEffectiveness));
        }
        return finalDamage;
    }

    /// <summary>
    /// Calculates the elemental damage based on the battle participant's elemental type
    /// and elemental attack type.
    /// </summary>
    /// <param name="attackElement">The elemental type of the attack.</param>
    /// <returns>The amount of damage</returns>
    public TypeEffectivenessResult CalculateElementalDamage(ElementType attackElement, int damage)
    {
        TypeEffectivenessResult result = new TypeEffectivenessResult();
        int damageToReturn = damage;
        switch (CurrentElementType)
        {
            case ElementType.Fire:
                if (attackElement == ElementType.Water)
                {
                    result.typeEffectiveness = DamageAnimationRecord.TypeEffectiveness.Weak;
                }
                if (attackElement == ElementType.Grass)
                {
                    result.typeEffectiveness = DamageAnimationRecord.TypeEffectiveness.Resist;
                }
                break;
            case ElementType.Water:
                if (attackElement == ElementType.Fire)
                {
                    result.typeEffectiveness = DamageAnimationRecord.TypeEffectiveness.Resist;
                }
                if (attackElement == ElementType.Grass)
                {
                    result.typeEffectiveness = DamageAnimationRecord.TypeEffectiveness.Weak;
                }
                break;
            case ElementType.Grass:
                if (attackElement == ElementType.Fire)
                {
                    result.typeEffectiveness = DamageAnimationRecord.TypeEffectiveness.Weak;
                }
                if (attackElement == ElementType.Water)
                {
                    result.typeEffectiveness = DamageAnimationRecord.TypeEffectiveness.Resist;
                }
                break;
        }
        if (result.typeEffectiveness == DamageAnimationRecord.TypeEffectiveness.Weak)
        {
            damageToReturn *= 2;
        }
        else if (result.typeEffectiveness == DamageAnimationRecord.TypeEffectiveness.Resist)
        {
            damageToReturn /= 2;
        }
        result.damage = damageToReturn;
        return result;
    }

    /// <summary>
    /// This handles when the BattleParticipant is healed.
    /// </summary>
    /// <param name="healAmount">The amount to heal.</param>
    public void Heal(int healAmount)
    {
        int finalHealAmount = Mathf.Min(healAmount, maxHp - currentHp);
        currentHp += finalHealAmount;
        if (finalHealAmount != 0)
        {
            BattleController.instance.AddDamageRecord(new DamageAnimationRecord(this, -finalHealAmount, DamageAnimationRecord.DamageType.HP));
        }
    }

    /// <summary>
    /// This handles when a BattleParticipant uses MP to use an Action.
    /// </summary>
    /// <param name="mp">The amount of MP this action cost.</param>
    public abstract void DrainMp(int mp);

    public void AddStatus(Status status)
    {
        var foundStatus = _statuses.Find(s => s.Equals(status));
        if (foundStatus!=null)
        {
            foundStatus.OnStatusRepeat();
            return;
        }
        Status newStatus = Instantiate<Status>(status, transform);
        _statuses.Add(newStatus);
        newStatus.AddStatus(this);
    }


    public void RemoveStatus(Status status)
    {
        _statuses.Remove(status);
        Destroy(status.gameObject);
    }

    public virtual void OnTurnEnd()
    {
        statuses.ForEach(status => status.OnTurnEnd());
    }

    public virtual bool CanUseAction(BattleAction action)
    {
        return true;
    }

    public void ChangeElementalType(ElementType newElementType)
    {
        currentElementType = newElementType;
    }

}
