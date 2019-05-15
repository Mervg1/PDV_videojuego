<<<<<<< .merge_file_NQOWlp
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public Text dialogueText;
    public Animator animator;
    public Player player;

    public Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        sentences = new Queue<string>();
    }


    public void StartDialogue(Dialogue dialogue)
    {

        animator.SetBool("StoryOpen", true);
        player.canMove = false;
        player.MoveAnimation(0);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            player.canMove = true;
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        //Debug.Log("End of the conversation");
        animator.SetBool("StoryOpen", false);
    }

}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public Text dialogueText;
    public Animator animator;
    public Player player;

    public Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {

        animator.SetBool("StoryOpen", true);
        player.canMove = false;
        player.MoveAnimation(0);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            player.canMove = true;
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        //Debug.Log("End of the conversation");
        animator.SetBool("StoryOpen", false);
    }

}
>>>>>>> .merge_file_A04odJ
