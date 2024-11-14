using UnityEngine;

public class EnemyForward : Enemy
{
    [Header("Movement Settings")]
    public float speed = 3f;  // Kecepatan enemy
    private Vector2 direction;  // Arah gerakan (vertikal)
    private float screenHalfHeight;  // Batas layar atas dan bawah

    private void Start()
    {
        // Menentukan batas atas dan bawah layar berdasarkan kamera utama
        screenHalfHeight = Camera.main.orthographicSize;

        // Menentukan arah awal berdasarkan posisi spawn
        if (transform.position.y >= screenHalfHeight)  // Jika spawn di atas
        {
            direction = Vector2.down;  // Gerak ke bawah
        }
        else if (transform.position.y <= -screenHalfHeight)  // Jika spawn di bawah
        {
            direction = Vector2.up;  // Gerak ke atas
        }
        else
        {
            // Jika spawn di tengah, pilih arah acak
            direction = Random.value > 0.5f ? Vector2.up : Vector2.down;
        }
    }

    private void Update()
    {
        // Gerakkan enemy secara vertikal
        transform.Translate(direction * speed * Time.deltaTime);

        // Balik arah ketika melewati batas layar atas atau bawah
        if (transform.position.y > screenHalfHeight)  // Batas atas layar
        {
            direction = Vector2.down;  // Balik ke bawah
        }
        else if (transform.position.y < -screenHalfHeight)  // Batas bawah layar
        {
            direction = Vector2.up;  // Balik ke atas
        }
    }
}
