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

    public void DealDamage(int damage)
    {
        currentHp = Mathf.Max(0, currentHp - damage);
    }

    public abstract void DrainMp(int mp);
}
