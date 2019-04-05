using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject Jimmy;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position - Jimmy.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Jimmy.transform.position + pos;
    }
}
