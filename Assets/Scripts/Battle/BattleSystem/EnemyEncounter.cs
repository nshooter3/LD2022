using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> enemies;
    [SerializeField]
    private Sprite characterSprite;
    [SerializeField]
    private bool finalBoss;
    public bool FinalBoss { get { return finalBoss; } }
    [SerializeField]
    private bool initialBattle;
    public bool InitialBattle { get { return initialBattle; } }
    public Sprite CharacterSprite { get { return characterSprite; } }

    public List<Enemy> SpawnEnemies()
    {
        List<Enemy> spawnedEnemies = new List<Enemy>();
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy tempEnemy = Instantiate<Enemy>(enemies[i]);
            spawnedEnemies.Add(tempEnemy);
            ScaleToUI scaleToUI = tempEnemy.gameObject.AddComponent<ScaleToUI>();
            scaleToUI.anchor = BattleUI.instance.GetEnemyAnchorPosition(i);
        }
        return spawnedEnemies;
    }
}
