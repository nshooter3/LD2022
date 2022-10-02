using UnityEngine;

public class BattleOrchestrator : MonoBehaviour
{
    public static BattleOrchestrator Instance { get; private set; }

    public EnemyEncounter currentEncounter;

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
}
