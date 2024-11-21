using UnityEngine;

public class EnemyBoss : Enemy
{
    [Header("Movement Settings")]
    public float speed = 3f;  // Kecepatan enemy
    private Vector2 direction;  // Arah gerakan
    private float screenHalfWidth;  // Batas layar

    private void Start()
    {
        // Menentukan batas layar berdasarkan kamera utama
        screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        // Menentukan arah awal berdasarkan posisi spawn
        if (transform.position.x <= -screenHalfWidth)  // Jika spawn di kiri
        {
            direction = Vector2.right;  // Gerak ke kanan
        }
        else if (transform.position.x >= screenHalfWidth)  // Jika spawn di kanan
        {
            direction = Vector2.left;  // Gerak ke kiri
        }
        else
        {
            direction = Random.value > 0.5f ? Vector2.right : Vector2.left;
        }
    }

    private void Update()
    {
        // Gerakkan enemy secara horizontal
        transform.Translate(direction * speed * Time.deltaTime);

        // Balik arah ketika melewati batas layar
        if (transform.position.x > screenHalfWidth)  // Batas kanan layar
        {
            direction = Vector2.left;
        }
        else if (transform.position.x < -screenHalfWidth)  // Batas kiri layar
        {
            direction = Vector2.right;
        }
    }
}
