using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Awake()
    {
        if (GameManager.LevelManager != null && GameManager.LevelManager != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    
    IEnumerator LoadSceneAsync(string sceneName)
    {
        if (animator != null)
        {
            animator.SetTrigger("StartTransition");
        }
        yield return new WaitForSeconds(1f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (animator != null)
        {
            animator.SetTrigger("EndTransition");
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}