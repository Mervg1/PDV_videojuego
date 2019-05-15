using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Dialogue enterDialogue;
    public Dialogue goFindKeyDialogue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                if(player.haveKey == true)
                {
                    TriggerDialogue(enterDialogue);
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
}
