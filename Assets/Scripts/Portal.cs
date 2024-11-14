using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;

    Vector2 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        // Inisialisasi nilai newPosition menggunakan ChangePosition()
        ChangePosition();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponPickup weaponPickup = Player.Instance.GetComponentInChildren<WeaponPickup>();

        if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>() != null)        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }

        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }

        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        // Check if the asteroid is close enough to the target position
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the asteroid collides with the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Asteroid collided with the player. Loading Main scene.");
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }

    void ChangePosition()
    {
        // Set newPosition to a random position within a specific range (example)
        newPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-8f, 8f));
    }
}