using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;     // Untuk menggunakan Object Pooling

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;
    public IObjectPool<Bullet> objectPool;

    public void Deactivate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        if (objectPool != null)
        {
            objectPool.Release(this);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        gameObject.SetActive(true);
        rb.velocity = new Vector2(0f, bulletSpeed) * transform.up;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        rb.velocity = new Vector2(0f, bulletSpeed) * transform.up;
    }

     private void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy)
        {
            Deactivate();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(gameObject.tag))
        {
            return;
        }
        Deactivate();
    }

    // [Header("Bullet Stats")]
    // [SerializeField]public float bulletSpeed = 10;
    // public int damage = 10;
    // private Rigidbody2D rb;
    
    // public IObjectPool<Bullet> objectPool;

    // private void Awake()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    // }

    // // private void FixedUpdate()
    // // {
    // //     rb.velocity = bulletSpeed * transform.up * Time.deltaTime;
    // // }

    // private void Update()
    // {
    //     Vector2 ppos = Camera.main.WorldToViewportPoint(transform.position);
        
    //     if (ppos.y >= 1.0f || ppos.y <= -0.01f && objectPool != null)
    //     {
    //         objectPool.Release(this);
    //     }
    // }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         other.gameObject.GetComponent<HitboxComponent>().Damage(this);
    //         objectPool.Release(this);
    //     }
    // }

    

    
}
