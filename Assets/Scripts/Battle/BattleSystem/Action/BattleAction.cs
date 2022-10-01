using UnityEngine;

public abstract class BattleAction : MonoBehaviour
{
    [SerializeField]
    private string actionName;
    public string ActionName { get { return actionName; } }

    public abstract void RunAction(BattleParticipant user, BattleParticipant enemy);
}
