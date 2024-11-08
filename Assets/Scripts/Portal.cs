using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;

    Vector3 newPosition;

    void Start()
    {
        ChangePosition();
    }

    void Update()
    {
        // Cek jarak posisi dan update posisi baru jika mendekati batas
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        // Tampilkan portal hanya jika player memiliki senjata
        bool playerHasWeapon = WeaponPickup.hasWeapon;
        GetComponent<SpriteRenderer>().enabled = playerHasWeapon;
        GetComponent<Collider2D>().enabled = playerHasWeapon;

        // Gerakan asteroid ke arah newPosition
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && WeaponPickup.hasWeapon)
        {
            if (GameManager.LevelManager != null)
            {
                GameManager.LevelManager.StartTransition(true);
            }
            else
            {
                Debug.LogError("LevelManager is null. Ensure it is initialized correctly.");
            }
        }
    }

    void ChangePosition()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        newPosition = new Vector3(x, y, transform.position.z);
    }
}
