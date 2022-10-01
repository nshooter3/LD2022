using UnityEngine;

public class Enemy : BattleParticipant
{
    [SerializeField]
    private BattleAction action;

    public override void ChooseAction()
    {
        currentAction = action;
    }

    public override void DrainMp(int mp)
    {
    }
}
