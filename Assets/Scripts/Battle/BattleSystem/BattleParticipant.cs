using System.Collections.Generic;
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

    public virtual void Initialize()
    {
        currentHp = maxHp;
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i] = Instantiate<BattleAction>(actions[i], transform);
        }
    }

    public abstract void ChooseAction();

    /// <summary>
    /// Handles when a Battle Participant attacks another battle participant.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="attackElement"></param>
    public void DealDamage(int damage, ElementType attackElement)
    {
        int elementalDamage = CalculateElementalDamage(attackElement, damage);
        currentHp = Mathf.Max(0, currentHp - elementalDamage);
    }

    public virtual void OnTurnEnd()
    {
    }

    /// <summary>
    /// Calculates the elemental damage based on the battle participant's elemental type
    /// and elemental attack type.
    /// </summary>
    /// <param name="attackElement">The elemental type of the attack.</param>
    /// <returns>The amount of damage</returns>
    public int CalculateElementalDamage(ElementType attackElement, int damage)
    {
        int damageToReturn = damage;
        switch (CurrentElementType)
        {
            case ElementType.Fire:
                if (attackElement == ElementType.Water)
                {
                    damageToReturn /= 2;
                }
                if (attackElement == ElementType.Grass)
                {
                    damageToReturn *= 2;
                }
                break;
            case ElementType.Water:
                if (attackElement == ElementType.Fire)
                {
                    damageToReturn *= 2;
                }
                if (attackElement == ElementType.Grass)
                {
                    damageToReturn /= 2;
                }
                break;
            case ElementType.Grass:
                if (attackElement == ElementType.Fire)
                {
                    damageToReturn /= 2;
                }
                if (attackElement == ElementType.Water)
                {
                    damageToReturn *= 2;
                }
                break;
        }
        return damageToReturn;
    }

    /// <summary>
    /// This handles when the BattleParticipant is healed.
    /// </summary>
    /// <param name="healAmount">The amount to heal.</param>
    public void Heal(int healAmount)
    {
        currentHp = Mathf.Min(maxHp, currentHp + healAmount);
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
