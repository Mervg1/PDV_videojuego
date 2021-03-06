﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Green : MonoBehaviour
{
    private float up, down;
    private bool upDown = true;
    public Animator transitionAnim;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        up = transform.position.y + 0.2f;
        down = transform.position.y - 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (transform.position.y <= down)
        {
            upDown = false;
        }

        if (transform.position.y >= up)
        {
            upDown = true;
        }

        if (upDown)
            transform.Translate(Vector3.down * 0.25f * Time.deltaTime);
        if (!upDown)
            transform.Translate(Vector3.up * 0.25f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.haveKey = true;
            }
            Destroy(this.gameObject);
        }
    }

    /*IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }*/
}
