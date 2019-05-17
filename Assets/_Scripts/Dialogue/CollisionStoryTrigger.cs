using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionStoryTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                TriggerDialogue();

            }
            Destroy(this.gameObject);
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<StoryManager>().StartDialogue(dialogue);
    }
}
