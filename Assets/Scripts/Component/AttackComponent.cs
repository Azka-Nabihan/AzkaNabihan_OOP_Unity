using UnityEngine;

[RequireComponent(typeof(Collider2D))]  // Memastikan objek memiliki Collider2D
public class AttackComponent : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Bullet bulletPrefab;  // Prefab Bullet yang akan digunakan (opsional)
    [SerializeField] private int damage = 10;      // Nilai damage yang diberikan

    // tidak terlalu diperlukan jika "Is Trigger" di kommponen Collider2D sudah diaktifkan
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
        if (other.gameObject.CompareTag(gameObject.tag)) return;

        // Cek apakah objek yang bertabrakan memiliki HitboxComponent
        if (other.GetComponent<HitboxComponent>() != null)
        {
            // 
            HitboxComponent hitbox = other.GetComponent<HitboxComponent>();

            if (bulletPrefab != null)
            {
                hitbox.Damage(bulletPrefab.damage);   // Damage dari bullet
            } else {
                hitbox.Damage(damage);  // Damage dari attack component
            }
        }

        if (other.GetComponent<InvincibilityComponent>() != null)
        {
            other.GetComponent<InvincibilityComponent>().TriggerInvincibility();
        }
    }
}



