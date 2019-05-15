using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintDrop : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    private bool right = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        /*if(right)
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(!right)
            transform.Translate(Vector3.up * speed * Time.deltaTime);*/
        StartCoroutine(live());
    }

    IEnumerator live()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
