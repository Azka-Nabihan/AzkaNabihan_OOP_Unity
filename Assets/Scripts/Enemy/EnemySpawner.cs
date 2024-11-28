using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0; // Jumlah musuh yang di-spawn saat ini
    public int defaultSpawnCount = 1; // Default jumlah musuh di awal
    public int spawnCountMultiplier = 1; // Faktor pengganda jumlah musuh
    public int multiplierIncreaseCount = 1; // Jumlah peningkatan pengganda

    public CombatManager combatManager; // Referensi ke CombatManager (jika ada)

    public bool isSpawning = false; // Status apakah spawner sedang aktif

    private void Start()
    {
        // Pastikan prefab musuh diatur 
        if (spawnedEnemy == null)
        {
            Debug.LogError("Prefab musuh belum diatur pada EnemySpawner.");
        }

        // Inisialisasi jumlah spawn awal
        spawnCount = defaultSpawnCount;
    }

    public void StartSpawning()
    {
        if (!isSpawning && spawnedEnemy.level <= combatManager.waveNumber)
            isSpawning = true; // Aktifkan mekanisme spawning
            StartCoroutine(SpawnEnemies());
            Debug.Log("Spawning dimulai.");
        
    }

    public IEnumerator SpawnEnemies()
    {
        if (isSpawning)
        {
            spawnCount = defaultSpawnCount;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            if(spawnedEnemy != null)
            {
                Enemy enemy = Instantiate(spawnedEnemy);
                enemy.GetComponent<Enemy>().combatManager = combatManager;
                enemy.GetComponent<Enemy>().enemySpawner = this;
                combatManager.totalEnemies++;
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        StopSpawning();
    }


    public void StopSpawning()
    {
        isSpawning = false; // Matikan mekanisme spawning
        StopAllCoroutines();
        Debug.Log("Spawning dihentikan.");
    }

    public void OnEnemyKilled()
    {
        Debug.Log("Enemy Killed");

        // call this method when an enemy is killed
        totalKill++; // Tambahkan total kill global
        totalKillWave++; // Tambahkan total kill dalam wave ini
        Debug.Log(totalKillWave);

        // Periksa apakah jumlah kill sudah cukup untuk meningkatkan spawn count
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            Debug.Log("Increase Spawn Count");
            totalKillWave = 0; // Reset total kill wave
            spawnCount = defaultSpawnCount + (spawnCountMultiplier * multiplierIncreaseCount);
            multiplierIncreaseCount++;
        }

        Debug.Log($"Total kill: {totalKill}, Total kill wave: {totalKillWave}");
    }
    
}
