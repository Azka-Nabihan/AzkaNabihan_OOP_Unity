using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Enemy Spawners")]
    public EnemySpawner[] enemySpawners; // Daftar spawner musuh

    [Header("Wave Settings")]
    public float timer = 0; // Timer untuk interval wave
    [SerializeField] private float waveInterval = 5f; // Jarak waktu antar wave
    public int waveNumber = 0; // Nomor wave saat ini
    public int totalEnemies = 0; // Total jumlah musuh di wave saat ini
    public int totalEnemiesKilled = 0; // Total jumlah musuh yang sudah dibunuh atau point


    // Dipanggil saat game dimulai
    void Start(){
        waveNumber = 0;
    }

    // Dipanggil setiap frame
    private void Update()
    {
        if(totalEnemies <= 0 && waveNumber >= 0){
            Debug.Log($"Timer berjalan: {timer}");
            timer += Time.deltaTime;
            if(timer >= waveInterval){
                StartNewWave();
            }
        }

    }

    private void StartNewWave()
    {
        Debug.Log($"Memulai wave {waveNumber}");

        timer = 0;
        waveNumber++;
        totalEnemies = 0;

        foreach(EnemySpawner enemySpawner in enemySpawners)
        {
            if(enemySpawner.spawnedEnemy.level <= waveNumber)
            {
                Debug.Log($"Spawning {enemySpawner.spawnedEnemy.name} pada wave {waveNumber}");
                enemySpawner.StartSpawning();
            }
        
        }
    }



    public void OnEnemyKilled(Enemy enemy)
    {
        totalEnemies--;
        totalEnemiesKilled += enemy.level;
    }
}
