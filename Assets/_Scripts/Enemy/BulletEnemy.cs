    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 15f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        StartCoroutine(live());
    }

    IEnumerator live()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        /*if(other.gameObject.tag == "Player")
        {
            //Destroy(other.gameObject);
        }*/
        if(other.gameObject.tag  == "Floor")
        {
            Destroy(this.gameObject);
        }
    }
}
