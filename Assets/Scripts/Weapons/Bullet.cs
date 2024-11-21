using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;     // Untuk menggunakan Object Pooling

public class Bullet : MonoBehaviour
{

    [Header("Bullet Stats")]
    [SerializeField]public float bulletSpeed = 10;
    public int damage = 10;
    private Rigidbody2D rb;
    
    public IObjectPool<Bullet> objectPool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // private void FixedUpdate()
    // {
    //     rb.velocity = bulletSpeed * transform.up * Time.deltaTime;
    // }

    private void Update()
    {
        Vector2 ppos = Camera.main.WorldToViewportPoint(transform.position);
        
        if (ppos.y >= 1.0f || ppos.y <= -0.01f && objectPool != null)
        {
            objectPool.Release(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<HitboxComponent>().Damage(this);
            objectPool.Release(this);
        }
    }
    
//    // Fungsi untuk mengatur referensi ObjectPool
//     public void SetObjectPool(IObjectPool<Bullet> pool)
//     {
//         objectPool = pool;
//     }

//     // Fungsi untuk menonaktifkan bullet seteleah timeout
//     public void Deactivate()
//     {
//         StartCoroutine(DeactivateRoutine(timeoutDelay));
//     }

//     // Coroutine untuk mnunggu dan kemudia menonakttifkan bullet
//     IEnumerator DeactivateRoutine(float delay)
//     {
//         yield return new WaitForSeconds(delay);

//         // reset rigidbody untuk mengehntikan pergerakan
//         Rigidbody2D rBody = GetComponent<Rigidbody2D>();
//         rBody.velocity = Vector2.zero;      // Hentikan gerakan

//         // release the projectile back to the pool
//         objectPool.Release(this);
//     }
    

    
}
