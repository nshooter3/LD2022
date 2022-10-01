using UnityEngine;

public class Enemy : BattleParticipant
{
    public override void ChooseAction()
    {
        currentAction = actions[0];
    }

    public override void DrainMp(int mp)
    {
    }
}
