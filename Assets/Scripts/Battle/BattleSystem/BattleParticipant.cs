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

    public StatusDisplay StatusPanel;

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
    public void DealDamage(int damage, ElementType attackElement)
    {
        int finalDamage = CalculateElementalDamage(attackElement, damage);
        foreach (Status status in statuses)
        {
            finalDamage = status.ModifyIncomingDamage(finalDamage);
        }
        currentHp = Mathf.Max(0, currentHp - finalDamage);
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
    /// This handles when a BattleParticipant is damaged by Recoil.
    /// NOTE: RECOIL DAMAGE IS NOT AFFECTED BY ELEMENTAL TYPE.
    /// </summary>
    /// <param name="damage">The amount to damage by.</param>
    public void DealRecoilDamage(int damage)
    {
        currentHp = Mathf.Max(0, currentHp - damage);
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
    /// This handles when a BattleParticipant uses MP to use an Action.
    /// </summary>
    /// <param name="mp">The amount of MP this action cost.</param>
    public abstract void DrainMp(int mp);

    public void AddStatus(Status status)
    {
        Status newStatus = Instantiate<Status>(status, transform);
        _statuses.Add(newStatus);
        newStatus.AddStatus(this);
        if (StatusPanel != null)
        {
            StatusPanel.DisplayStatus();
        }
    }

    public void RemoveStatus(Status status)
    {
        _statuses.Remove(status);
        Destroy(status.gameObject);
        if (StatusPanel != null)
        {
            StatusPanel.DisplayStatus();
        }
    }

    public virtual void OnTurnEnd()
    {
        statuses.ForEach(status => status.OnTurnEnd());
        if (StatusPanel != null)
        {
            StatusPanel.DisplayStatus();
        }
    }
}
