using UnityEngine;

public abstract class BattleParticipant : MonoBehaviour
{
    public int currentHp { get; private set; }
    [SerializeField]
    private int maxHp;

    public BattleAction currentAction { get; protected set; }

    public bool Dead { get { return currentHp <= 0; } }

    public virtual void Initialize()
    {
        currentHp = maxHp;
    }

    public abstract void ChooseAction();

    public void DealDamage(int damage)
    {
        currentHp = Mathf.Max(0, currentHp - damage);
    }

    public abstract void DrainMp(int mp);
}
