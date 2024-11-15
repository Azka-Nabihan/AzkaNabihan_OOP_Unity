using UnityEngine;
using UnityEngine.Assertions;

public class EnemyClickSpawner : MonoBehaviour
{
    // Array untuk menyimpan varian enemy yang dapat di-spawn
    [SerializeField] private Enemy[] enemyVariants;

    // Indeks untuk menyimpan varian musuh yang saat ini dipilih
    [SerializeField] private int selectedVariant = 0;

    // Fungsi yang dijalankan pada awal permainan
    void Start()
    {
        // Pastikan array enemyVariants tidak kosong
        Assert.IsTrue(enemyVariants.Length > 0, "Tambahkan setidaknya 1 Prefab Enemy terlebih dahulu!");
    }

    // Fungsi yang dipanggil setiap frame
    private void Update()
    {
        // Periksa input angka untuk memilih varian musuh
        for (int i = 1; i <= enemyVariants.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString())) // Jika tombol angka ditekan
            {
                selectedVariant = i - 1; // Pilih varian musuh berdasarkan indeks
                Debug.Log($"Varian musuh yang dipilih: {selectedVariant}");
            }
        }

        // Periksa klik kanan untuk spawn musuh
        if (Input.GetMouseButtonDown(1)) // Jika tombol mouse kanan ditekan
        {
            SpawnEnemy();
        }
    }

    // Fungsi untuk memunculkan musuh
    private void SpawnEnemy()
    {
        if (selectedVariant < enemyVariants.Length)
        {
            // Tentukan lokasi spawn acak
            float spawnX = Random.Range(-8f, 8f);
            float spawnY = Random.Range(-4f, 4f);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

            // Spawn musuh
            Instantiate(enemyVariants[selectedVariant], spawnPosition, Quaternion.identity);
            Debug.Log($"Musuh varian {selectedVariant} di-spawn di posisi {spawnPosition}");
        }
    }
}
