using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    public bool right = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (right)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (!right)
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        StartCoroutine(live());
    }

    IEnumerator live()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
