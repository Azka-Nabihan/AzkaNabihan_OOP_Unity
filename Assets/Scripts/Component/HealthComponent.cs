using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]
    private int maxHealth = 10;  // Maksimal nilai health yang bisa diatur di Inspector
    private int health;  // Nilai health saat ini

    private void Start()
    {
        // Inisialisasi nilai health dengan nilai maxHealth saat objek pertama kali diaktifkan
        health = maxHealth;
    }

    // Getter untuk mendapatkan nilai health saat ini
    public int GetHealth()
    {
        return health;
    }

    // Method untuk mengurangi health
    public void Subtract(int damage)
    {
        health -= damage;  // Kurangi health dengan jumlah damage yang diterima

        // Cek jika health kurang dari atau sama dengan 0
        if (health <= 0)
        {
            Destroy(gameObject);  // Hancurkan objek ini dari scene
        }
    }

}
