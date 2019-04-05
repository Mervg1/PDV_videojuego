using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Enemy
{
    private Vector3 _currentTarget;
    [SerializeField] private GameObject bulletPrefab;
    private BulletEnemy bullet;
    private bool canShoot = true;
    private bool right = true;

    // Start is called before the first frame update
    private void Start()
    {
        bullet = bulletPrefab.GetComponent<BulletEnemy>();
    }

    // Update is called once per frame
    public override void Update()
    {
        Movement();
        shoot();
    }

    void Movement()
    {
        if(transform.position.x == pointA.position.x)
        {
            _currentTarget = pointB.position;
            right = true;
            bullet.right = true;
        }else if(transform.position.x == pointB.position.x)
        {
            _currentTarget = pointA.position;
            right = false;
            bullet.right = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }

    private void shoot()
    {
        if (canShoot)
        {
            if(right)
                Instantiate(bulletPrefab, transform.position + new Vector3(0.97f, 1.14f, 0), Quaternion.Euler(0f, 0f, 40f));
            if(!right)
                Instantiate(bulletPrefab, transform.position + new Vector3(-0.97f, 1.14f, 0), Quaternion.Euler(0f, 0f, 40f));
            canShoot = false;
            StartCoroutine(waitForShoot());
        }
    }

    IEnumerator waitForShoot()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
