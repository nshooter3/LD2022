using UnityEngine;

public class EnemyPosition : MonoBehaviour
{
    public bool available = true;
    public Enemy enemy;

    private void Update()
    {
        if (enemy != null && enemy.Dead)
        {
            enemy = null;
            available = true;
        }
    }
}
