using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Awake()
    {
        GameObject someObject = GameObject.Find("Camera");
        if (someObject != null)
        {
            someObject.SetActive(false);
        }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {

        animator.SetTrigger("Start");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadSceneAsync(sceneName);

        Player.Instance.transform.position = new Vector3(0, -4.5f, 0);

        animator.SetTrigger("End");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}