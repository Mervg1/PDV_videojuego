using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Dialogue enterDialogue;
    public Dialogue goFindKeyDialogue;
    public Animator transitionAnim;
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                if(player.haveKey == true)
                {
                    //TriggerDialogue(enterDialogue);
                    StartCoroutine(LoadScene());
                }
                else
                {
                    TriggerDialogue(goFindKeyDialogue);
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
