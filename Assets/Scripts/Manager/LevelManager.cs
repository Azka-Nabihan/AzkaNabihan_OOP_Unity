using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Awake()
    {
        animator.enabled = false;
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Aktifkan animator dan mainkan animasi StartTransition
        animator.enabled = true;

        // Tunggu hingga animasi StartTransition selesai
        yield return new WaitForSeconds(1f); // Sesuaikan waktu ini dengan durasi animasi StartTransition

        // Memuat scene baru secara asinkron
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Tunggu hingga scene selesai dimuat
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        
        // Mainkan animasi EndTransition
        animator.SetTrigger("EndTransition");
    }

    // Fungsi publik untuk memulai transisi dan load scene
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}