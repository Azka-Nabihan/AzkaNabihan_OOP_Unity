using UnityEngine;

[RequireComponent(typeof(Collider2D))]  // Memastikan objek memiliki Collider2D
public class HitboxComponent : MonoBehaviour
{
    [Header("Health Reference")]
    [SerializeField] 
    private HealthComponent healthComponent;  // Referensi ke HealthComponent

    private void Awake()
    {
        // Jika healthComponent tidak diset di Inspector, coba cari HealthComponent pada objek ini
        if (healthComponent == null)
        {
            healthComponent = GetComponent<HealthComponent>();

            // Jika HealthComponent tetap tidak ditemukan, beri peringatan
            if (healthComponent == null)
            {
                Debug.LogWarning("HealthComponent tidak ditemukan pada objek " + gameObject.name);
            }
        }
    }

    // Method Damage untuk menerima damage dari objek Bullet
    public void Damage(Bullet bullet)
    {
        if (healthComponent != null)
        {
            // Mengurangi health menggunakan nilai damage dari bullet
            healthComponent.Subtract(bullet.damage);
        }
    }

    // Method Damage untuk menerima damage dalam bentuk integer
    public void Damage(int damageAmount)
    {
        if (healthComponent != null)
        {
            // Mengurangi health dengan nilai damage yang diberikan
            healthComponent.Subtract(damageAmount);
        }
    }
}
