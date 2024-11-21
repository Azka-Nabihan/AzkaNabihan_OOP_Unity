using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Enemy Spawners")]
    public EnemySpawner[] enemySpawners; // Daftar spawner musuh

    [Header("Wave Settings")]
    public float timer = 0; // Timer untuk interval wave
    [SerializeField] private float waveInterval = 5f; // Jarak waktu antar wave
    public int waveNumber = 1; // Nomor wave saat ini
    public int totalEnemies = 0; // Total jumlah musuh di wave saat ini

    private bool isWaveActive = false; // Status apakah wave sedang berlangsung

    private void Update()
    {
        // Hitung timer hanya jika wave tidak aktif
        if (!isWaveActive)
        {
            timer += Time.deltaTime;

            if (timer >= waveInterval)
            {
                StartNewWave(); // Mulai wave baru
                timer = 0; // Reset timer untuk wave berikutnya
            }
        }

        // Periksa apakah semua musuh di wave saat ini sudah dikalahkan
        if (isWaveActive && totalEnemies <= 0)
        {
            EndWave(); // Akhiri wave
        }
    }

    private void StartNewWave()
    {
        Debug.Log($"Memulai wave {waveNumber}");

        isWaveActive = true;

        // Reset jumlah musuh
        totalEnemies = 0;

        // Aktifkan semua spawner untuk wave ini
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
            {
                // Perbarui jumlah spawn sesuai wave
                spawner.spawnCount = spawner.defaultSpawnCount + (spawner.spawnCountMultiplier * (waveNumber - 1));

                // Mulai spawning
                spawner.StartSpawning();

                // Tambahkan jumlah musuh yang di-spawn ke totalEnemies
                totalEnemies += spawner.spawnCount;
            }
        }
    }

    private void EndWave()
    {
        Debug.Log($"Wave {waveNumber} selesai.");

        isWaveActive = false;
        waveNumber++; // Naikkan nomor wave

        // Berhentikan semua spawner
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.StopSpawning();
            }
        }
    }

    public void OnEnemyKilled()
    {
        // Panggil setiap kali musuh terbunuh
        totalEnemies--; // Kurangi jumlah musuh
        Debug.Log($"Musuh tersisa: {totalEnemies}");

        // Pastikan tidak ada musuh tersisa
        if (totalEnemies <= 0 && isWaveActive)
        {
            EndWave();
        }
    }
}
