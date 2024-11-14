using UnityEngine;

[RequireComponent(typeof(Collider2D))]  // Memastikan objek memiliki Collider2D
public class AttackComponent : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Bullet bulletPrefab;  // Prefab Bullet yang akan digunakan (opsional)
    [SerializeField] private int damage = 10;      // Nilai damage yang diberikan

    private void Awake()
    {
        // Pastikan Collider2D adalah trigger
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah objek yang bertabrakan memiliki tag yang sama
        if (other.CompareTag(gameObject.tag))
        {
            return;  // Jika tag sama, batalkan operasi
        }

        // Cek apakah objek yang bertabrakan memiliki HitboxComponent
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            // Jika ada HitboxComponent, berikan damage
            hitbox.Damage(damage);
        }
    }
}
