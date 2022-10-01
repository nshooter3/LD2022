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

    /// <summary>
    /// The elemental type of the battle participant
    /// </summary>
    public ElementType currentElementType { get; protected set; }

    public bool Dead { get { return currentHp <= 0; } }

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
        int elementalDamage = CalculateElementalDamage(attackElement, damage);
        currentHp = Mathf.Max(0, currentHp - elementalDamage);
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
        switch (currentElementType)
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
