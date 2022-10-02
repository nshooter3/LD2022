using System.Collections.Generic;
using UnityEngine;

public class EnemyPositions : MonoBehaviour
{
    [SerializeField]
    private List<EnemyPosition> enemyPositions;

    public static EnemyPositions instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public EnemyPosition GetAvailablePosition(Enemy enemy)
    {
        foreach(EnemyPosition enemyPos in enemyPositions)
        {
            if (enemyPos.available)
            {
                enemyPos.enemy = enemy;
                enemyPos.available = false;
                return enemyPos;
            }
        }
        return null;
    }
}
