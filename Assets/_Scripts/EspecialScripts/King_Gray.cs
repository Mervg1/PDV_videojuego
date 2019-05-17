using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King_Gray : MonoBehaviour
{
    private float up, down;
    private bool upDown = true;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float fTransition = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        up = transform.position.y + fTransition;
        down = transform.position.y - fTransition;
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
            transform.Translate(Vector3.down * 0.25f * Time.deltaTime* speed);
        if (!upDown)
            transform.Translate(Vector3.up * 0.25f * Time.deltaTime * speed);
    }
}
