using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    private bool leftRight = true;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float pointA = 0;
    [SerializeField] private float pointB = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (transform.position.x <= pointA)
        {
            leftRight = false;
        }

        if (transform.position.x >= pointB)
        {
            leftRight = true;
        }

        if (leftRight)
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        if (!leftRight)
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
