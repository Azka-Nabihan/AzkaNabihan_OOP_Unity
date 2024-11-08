using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Awake()
    {
        if (animator == null)
        {
            animator = GameObject.Find("SceneTransition").GetComponent<Animator>();
        }
    }

    public void StartTransition(bool hitPortal)
    {
        if (hitPortal)
        {
            animator.SetTrigger("StartTransition");
            Debug.Log("Animasi transisi dimulai.");
        }
    }

    public void OnTransitionComplete()
    {
        Debug.Log("Transisi selesai, memuat scene 'Main'.");
        LoadScene("Main");
    }

    private void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    { 
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
