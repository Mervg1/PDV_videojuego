using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

    public void DisplayNextScene()
    {
        StartCoroutine(LoadScene());
        Debug.Log("HOLA");
    }
}
