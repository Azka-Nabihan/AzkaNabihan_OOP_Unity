using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;     // Untuk menggunakan Object Pooling

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    public Transform parentTransform;
    private float nextTimeToShoot = 0f;  // Variabel untuk menandai waktu tembakan berikutnya


    private void Awake()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    // Invoked when creating an time to populate the object pool
    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bullet); // Instantiasi Bullet
        bulletInstance.SetObjectPool(objectPool);     // Mengatur ObjectPool
        return bulletInstance;
    }

    // invoked when retrieving the next item from the object pool
    private void OnGetFromPool(Bullet bulletObject)
    {
        bulletObject.gameObject.SetActive(true);  // Aktifkan bullet
        bulletObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;  // Reset kecepatan bullet
    }

    // invoked when returning an item to the object pool
    private void OnReleaseToPool(Bullet bulletObject)
    {
        bulletObject.gameObject.SetActive(false);   // Nonaktifkan bullet
    }

    // Invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(Bullet bulletObject)
    {
        Destroy(bulletObject.gameObject);
    }

    private void Update()
    {
        // Periksa apakah tombol tembak ditekan dan sudah waktunya menembak
        if (Time.time >= nextTimeToShoot)
        {
            FireBullet();
            // Set waktu cooldown tembakan
            nextTimeToShoot = Time.time + shootIntervalInSeconds;
        }
    }

    private void FireBullet()
    {
        // Amibl bullet dari object pool
        Bullet bulletObject = objectPool.Get();

        if(bulletObject == null)
            return; 

        // Set posisi dan rotasi bullet agar sesuai dengan posisi spawn point senjata saat ini
        bulletObject.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Akses bulletSpeed dari Bullet.cs dan terapkan ke kecepatan
        float bulletSpeed = bulletObject.bulletSpeed;

        // Berikan kecepatan agar bullet bergerak ke depan
        bulletObject.GetComponent<Rigidbody2D>().velocity = bulletObject.transform.up * bulletSpeed;

        // Kembalikan bullet ke pool setelah waktu tertentu
        bulletObject.Deactivate();
    }
}
