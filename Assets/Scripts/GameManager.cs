using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }  // Singleton Instance
    public static LevelManager LevelManager { get; private set; }  // Level manager
    

    void Awake(){
        // Check if an Instance of GameManager already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy this Instance if one already exists
            return;
        }

        Instance = this; // Set the singleton instance to this GameManager
        DontDestroyOnLoad(gameObject); // Persist this GameManager across scenes

        // Initialize the LevelManager from child components
        LevelManager = GetComponentInChildren<LevelManager>();

        LevelManager = FindObjectOfType<LevelManager>();
        if (LevelManager == null)
        {
            Debug.LogError("LevelManager tidak ditemukan di scene.");
        }
        else
        {
            Debug.Log("LevelManager berhasil diinisialisasi.");
        }

        // Ensure that the Main Camera is preserved across scenes
        GameObject mainCamera = GameObject.FindWithTag("Main Camera");
        if (mainCamera != null)
        {
            DontDestroyOnLoad(mainCamera);
        }

         if (LevelManager == null)
        {
            Debug.LogError("LevelManager tidak ditemukan di GameManager. Pastikan LevelManager adalah child dari GameManager.");
        }
    }
}

