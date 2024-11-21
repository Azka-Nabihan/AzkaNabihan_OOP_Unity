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

    private float timer = 0f; // Timer untuk mengatur interval spawn

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

    private void Update()
    {
        if (isSpawning)
        {
            // Hitung waktu untuk mekanisme spawn interval
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                SpawnEnemies(); // Panggil fungsi spawn
            }
        }
    }

    private void SpawnEnemies()
    {
        if (spawnedEnemy == null)
        {
            Debug.LogError("Prefab musuh belum diatur.");
            return;
        }

        // Spawn musuh sebanyak spawnCount
        for (int i = 0; i < spawnCount; i++)
        {
            // Tentukan lokasi spawn acak
            float spawnX = Random.Range(-8f, 8f); // Sesuaikan dengan ukuran arena
            float spawnY = Random.Range(-4f, 4f);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

            // Spawn musuh
            Instantiate(spawnedEnemy, spawnPosition, Quaternion.identity);
            Debug.Log($"Musuh di-spawn di posisi {spawnPosition}");
        }

        // Reset jumlah total kill dalam satu wave
        totalKillWave = 0;
    }

    public void OnEnemyKilled()
    {
        totalKill++; // Tambahkan total kill global
        totalKillWave++; // Tambahkan total kill dalam wave ini

        // Periksa apakah jumlah kill sudah cukup untuk meningkatkan spawn count
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            IncreaseSpawnCount();
        }

        Debug.Log($"Total kill: {totalKill}, Total kill wave: {totalKillWave}");
    }

    private void IncreaseSpawnCount()
    {
        spawnCount += multiplierIncreaseCount * spawnCountMultiplier; // Tingkatkan jumlah spawn
        Debug.Log($"Jumlah musuh yang di-spawn meningkat menjadi {spawnCount}");
    }

    public void StartSpawning()
    {
        isSpawning = true; // Aktifkan mekanisme spawning
        Debug.Log("Spawning dimulai.");
    }

    public void StopSpawning()
    {
        isSpawning = false; // Matikan mekanisme spawning
        Debug.Log("Spawning dihentikan.");
    }
}
