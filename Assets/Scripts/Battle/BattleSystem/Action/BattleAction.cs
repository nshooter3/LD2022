using UnityEngine;

public abstract class BattleAction : MonoBehaviour
{
    [SerializeField]
    private string actionName;

    public abstract void RunAction(BattleParticipant user, BattleParticipant enemy);
}
