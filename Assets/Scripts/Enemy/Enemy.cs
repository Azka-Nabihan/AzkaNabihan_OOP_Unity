using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int level = 1;  // Level enemy yang bisa diatur di Inspector
    protected Rigidbody2D rb;  // Referensi ke Rigidbody2D
    protected Transform player;  // Referensi ke posisi player

    private void Awake()
    {
        // Menambahkan Rigidbody2D jika belum ada dan mengatur physics
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0;  // Nonaktifkan gravitasi
        rb.freezeRotation = true;  // Cegah rotasi agar enemy tetap stabil

        // Mencari player di scene dengan tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player tidak ditemukan. Pastikan ada objek dengan tag 'Player'.");
        }
    }

    private void Update()
    {
        // Jika player ditemukan, atur rotasi enemy agar menghadap player
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
    }
}
