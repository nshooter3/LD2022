using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> enemies;
    [SerializeField]
    private Sprite characterSprite;
    public Sprite CharacterSprite { get { return characterSprite; } }

    public List<Enemy> SpawnEnemies()
    {
        List<Enemy> spawnedEnemies = new List<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            Enemy tempEnemy = Instantiate<Enemy>(enemy);
            spawnedEnemies.Add(tempEnemy);
            EnemyPosition enemyPos = EnemyPositions.instance.GetAvailablePosition(enemy);
            if (enemyPos != null)
            {
                tempEnemy.transform.position = enemyPos.transform.position;
            }
            else
            {
                Debug.LogError("No position available for " + tempEnemy.name + ". Please ensure that you're not spawning too many enemies.");
            }
        }
        return spawnedEnemies;
    }
}
