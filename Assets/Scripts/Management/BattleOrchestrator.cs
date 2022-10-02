using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BattleOrchestrator : MonoBehaviour
{
    public static BattleOrchestrator Instance { get; private set; }

    public EnemyEncounter currentEncounter;
    [SerializeField]
    private List<BattleAction> startingActions;
    [SerializeField]
    private List<BattleAction> unlockableActions;
    public List<BattleAction> currentActions { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        currentActions = new List<BattleAction>(startingActions);
    }

    public void AddAction(BattleAction action)
    {
        currentActions.Add(action);
    }

    public List<BattleAction> GetRandomNewActions(int numActions)
    {
        HashSet<BattleAction> currentActionSet = new HashSet<BattleAction>(currentActions);
        List<BattleAction> newActions = unlockableActions.FindAll(action => !currentActionSet.Contains(action));
        if (newActions.Count <= numActions)
        {
            return newActions;
        }
        List<BattleAction> chosenActions = new List<BattleAction>();
        while (chosenActions.Count < numActions)
        {
            int randomIndex = Random.Range(0, newActions.Count);
            if (!chosenActions.Contains(newActions[randomIndex]))
            {
                chosenActions.Add(newActions[randomIndex]);
            }
        }
        return chosenActions;
    }
}
