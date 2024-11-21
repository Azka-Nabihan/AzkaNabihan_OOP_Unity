using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton instance
    public static Player Instance { get; private set; }
    PlayerMovement playerMovement;
    Animator animator;

    // Event function Awake
    void Awake()
    {
        // Cek jika Instance sudah di-set
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Hapus duplikat Player jika sudah ada instance
            return;
        }

        // Set instance ke objek ini
        Instance = this;

        // Pastikan Player tidak ter-destroy saat pindah Scene
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Mengambil informasi dari Script PlayerMovement dan menyimpannya di playerMovement
        playerMovement = GetComponent<PlayerMovement>();

        // Mengambil informasi dari Animator dari EngineEffect dan menyimpannya di animator
        animator = GameObject.Find("EngineEffect").GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Memanggil method Move dari PlayerMovement
        playerMovement.Move();
    }

    void LateUpdate()
    {
        playerMovement.MoveBound();
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}
