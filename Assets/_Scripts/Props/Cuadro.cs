using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadro : MonoBehaviour
{
    private Quaternion startPos, lastPos;
    private bool cond;
    private int speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        startPos = Quaternion.Euler(0, 0, -30);
        lastPos = Quaternion.Euler(0, 0, 30);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Lerp(startPos, lastPos, Mathf.Sin(Time.time * speed));
    }

}
