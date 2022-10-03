using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BattleOrchestrator : MonoBehaviour
{
    public static BattleOrchestrator Instance { get; private set; }

    [SerializeField]
    private List<EnemyEncounter> allEncounters;
    public EnemyEncounter currentEncounter;
    private EnemyEncounter initialEncounter;
    private HashSet<EnemyEncounter> completedEncounters = new HashSet<EnemyEncounter>();
    public const string finalBossEncounterName = "Time Master";
    public bool finalBossUnlocked { get; private set; }

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
        initialEncounter = currentEncounter;
        Reset();

    }

    public void AddAction(BattleAction action)
    {
        currentActions.Add(action);
    }

    public List<BattleAction> GetRandomNewActions(int numActions)
    {
        HashSet<BattleAction> currentActionSet = new HashSet<BattleAction>(currentActions);
        List<BattleAction> newActions = unlockableActions.FindAll(action => !currentActionSet.Contains(action));
        return RandomUtil.ChooseRandomElementsFromList(newActions, numActions);
    }

    public void CompleteEncounter()
    {
        completedEncounters.Add(currentEncounter);
        finalBossUnlocked = allEncounters.All(encounter => EncounterCompleted(encounter) || encounter.FinalBoss);
    }

    public bool EncounterCompleted(EnemyEncounter encounter)
    {
        return completedEncounters.Contains(encounter);
    }

    public List<EnemyEncounter> GetAvailableEncounters()
    {
        if (finalBossUnlocked)
        {
            List<EnemyEncounter> encounters = new List<EnemyEncounter>();
            encounters.Add(allEncounters.Find(encounter => encounter.FinalBoss));
            return encounters;
        }
        return allEncounters;
    }

    public void Reset()
    {
        currentActions = new List<BattleAction>(startingActions);
        completedEncounters.Clear();
        finalBossUnlocked = false;
        currentEncounter = initialEncounter;
    }
}
