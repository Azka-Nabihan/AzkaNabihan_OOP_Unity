using UnityEngine;
using UnityEngine.Rendering;

public class EnemyTargeting : Enemy
{
    [Header("Movement Settings")]
    public float speed = 3f;  // Kecepatan pergerakan enemy
    private Transform player;  // Referensi ke player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // Cari player di scene
    }

    private void Update()
    {
        // Pastikan player tidak null sebelum bergerak
        if (player != null)
        {
            // Hitung arah menuju player
            Vector2 direction = (player.position - transform.position).normalized;
            
            // Gerakkan enemy ke arah player
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jika bertabrakan dengan player, maka enemy menghilang
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);  // Hapus enemy dari scene
        }
    }
}
