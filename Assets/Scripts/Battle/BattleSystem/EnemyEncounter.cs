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
            spawnedEnemies.Add(Instantiate<Enemy>(enemy));
        }
        return spawnedEnemies;
    }
}
