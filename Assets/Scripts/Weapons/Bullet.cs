using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;     // Untuk menggunakan Object Pooling

public class Bullet : MonoBehaviour
{

    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;
    
    [SerializeField] private float timeoutDelay = 3f;
    private IObjectPool<Bullet> objectPool;
    
   // Fungsi untuk mengatur referensi ObjectPool
    public void SetObjectPool(IObjectPool<Bullet> pool)
    {
        objectPool = pool;
    }

    // Fungsi untuk menonaktifkan bullet seteleah timeout
    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    // Coroutine untuk mnunggu dan kemudia menonakttifkan bullet
    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // reset rigidbody untuk mengehntikan pergerakan
        Rigidbody2D rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = Vector2.zero;      // Hentikan gerakan

        // release the projectile back to the pool
        objectPool.Release(this);
    }
    
}
