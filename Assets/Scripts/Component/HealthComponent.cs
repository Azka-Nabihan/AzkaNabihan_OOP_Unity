using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]
    private int maxHealth = 100;  // Maksimal nilai health yang bisa diatur di Inspector

    private int health;  // Nilai health saat ini

    // Getter untuk mendapatkan nilai health saat ini
    public int Health
    {
        get { return health; }
    }

    private void Awake()
    {
        // Inisialisasi nilai health dengan nilai maxHealth saat objek pertama kali diaktifkan
        health = maxHealth;
    }

    // Method untuk mengurangi health
    public void Subtract(int damage)
    {
        health -= damage;  // Kurangi health dengan jumlah damage yang diterima

        // Cek jika health kurang dari atau sama dengan 0
        if (health <= 0)
        {
            health = 0;  // Pastikan health tidak negatif
            DestroyEntity();  // Panggil fungsi untuk menghancurkan objek
        }
    }

    // Method untuk menghancurkan objek
    private void DestroyEntity()
    {
        Destroy(gameObject);  // Hancurkan objek ini dari scene
    }
}
