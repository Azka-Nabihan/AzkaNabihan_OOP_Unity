using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder; 
    private Weapon weapon;
    public static bool hasWeapon = false;  

    void Awake()
    {
        weapon = null;
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Weapon existingWeapon = other.GetComponentInChildren<Weapon>();

            if (existingWeapon != null)
            {
                Debug.Log("Menghancurkan senjata lama.");
                Destroy(existingWeapon.gameObject);
            }

            weapon = Instantiate(weaponHolder, other.transform.position, Quaternion.identity);
            weapon.transform.SetParent(other.transform);
            TurnVisual(true, weapon);
            hasWeapon = true;
            Debug.Log("Senjata baru dipasang.");
        }
    }

    void TurnVisual(bool on)
    {
        foreach (var renderer in weapon.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = on;
        }

        foreach (var collider in weapon.GetComponentsInChildren<Collider>())
        {
            collider.enabled = on;
        }
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        foreach (var renderer in weapon.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = on;
        }

        foreach (var collider in weapon.GetComponentsInChildren<Collider>())
        {
            collider.enabled = on;
        }
    }
}
