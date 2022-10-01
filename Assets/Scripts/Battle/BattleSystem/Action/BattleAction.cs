using UnityEngine;

public abstract class BattleAction : MonoBehaviour
{
    [SerializeField]
    private string actionName;
    public string ActionName { get { return actionName; } }

    [SerializeField]
    private bool areaOfEffect;
    public bool AreaOfEffect { get { return areaOfEffect; } }

    public abstract void RunAction(BattleParticipant user, BattleParticipant enemy);

    public abstract string GetIntentDisplay();
}
