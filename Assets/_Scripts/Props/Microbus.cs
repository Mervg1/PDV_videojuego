using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Microbus : MonoBehaviour
{
    public Dialogue enterDialogue;
    public Dialogue goFindPaintDialogue;
    public Animator transitionAnim;
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                if(player.havePaint == true)
                {
                    //TriggerDialogue(enterDialogue);
                    StartCoroutine(LoadScene());
                }
                else
                {
                    TriggerDialogue(goFindPaintDialogue);
                }
            }
        }
    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        FindObjectOfType<StoryManager>().StartDialogue(dialogue);
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}

