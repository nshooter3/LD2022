using System.Collections.Generic;
using UnityEngine;
using static ElementTypes;

public abstract class BattleParticipant : MonoBehaviour
{
    public int currentHp { get; private set; }
    [SerializeField]
    private int maxHp;
    public int MaxHp { get { return maxHp; } }

    public BattleAction currentAction { get; protected set; }

    [SerializeField]
    protected List<ElementType> Weaknesses = new List<ElementType>();

    [SerializeField]
    protected List<ElementType> Immunities = new List<ElementType>();

    [SerializeField]
    protected List<ElementType> Resistances = new List<ElementType>();

    public bool IsDead { get { return currentHp <= 0; } }

    public virtual void Initialize()
    {
        currentHp = maxHp;
    }

    public abstract void ChooseAction();

    /// <summary>
    /// Handles when a Battle Participant attacks another battle participant.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="attackElement"></param>
    public void DealDamage(int damage, ElementType attackElement)
    {
        if (Weaknesses.Contains(attackElement))
        {
            damage *= 2;
        }
        else if (Resistances.Contains(attackElement))
        {
            damage /= 2;
            Mathf.FloorToInt(damage);
        }
        else if (Immunities.Contains(attackElement))
        {
            damage = 0;
        }
        currentHp = Mathf.Max(0, currentHp - damage);
    }

    /// <summary>
    /// This handles when a BattleParticipant is damaged by Recoil.
    /// NOTE: RECOIL DAMAGE IS NOT AFFECTED BY ELEMENTAL TYPE.
    /// </summary>
    /// <param name="damage">The amount to damage by.</param>
    public void DealRecoilDamage(int damage)
    {
        currentHp = Mathf.Max(0, currentHp - damage);
    }

    /// <summary>
    /// This handles when a BattleParticipant uses MP to use an Action.
    /// </summary>
    /// <param name="mp">The amount of MP this action cost.</param>
    public abstract void DrainMp(int mp);
}
