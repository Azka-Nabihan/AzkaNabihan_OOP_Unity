using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int level;// Level enemy yang bisa diatur di Inspector
    public EnemySpawner enemySpawner;  // Referensi ke EnemySpawner yang membuat enemy ini
    public CombatManager combatManager;  // Referensi ke CombatManager

    private void OnDestroy()
    {
        if (enemySpawner != null && combatManager != null)
        {
            enemySpawner.OnEnemyKilled();
            combatManager.OnEnemyKilled(this);
        }
    }
}
