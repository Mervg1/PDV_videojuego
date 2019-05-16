using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private Vector3 _currentTarget;
    private BulletEnemy bullet;
    private bool canShoot = true;
    private bool right = false;
    private bool canMove = true;
    private Animator _anim;
    private SpriteRenderer _sprite;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] protected Transform pointA, pointB;
    [SerializeField] protected int speed;

    // Start is called before the first frame update
    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = bulletPrefab.GetComponent<BulletEnemy>();
        _currentTarget = pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        shoot();
    }

    void Movement()
    {
        if (_currentTarget == pointA.position)
        {
            _sprite.flipX = true;
        }
        else
        {
            _sprite.flipX = false;
        }

        if ((transform.position.x == pointA.position.x) || (transform.position.x == pointB.position.x))
        {
            MoveAnimation(false);
            canMove = false;
            StartCoroutine(WaitToTurn());
        }

        if (canMove)
        {
            MoveAnimation(true);
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }

    private void shoot()
    {
        if (canShoot)
        {
            if(right)
                Instantiate(bulletPrefab, transform.position + new Vector3(1f, 0.8f, 0), Quaternion.Euler(0f, 0f, 90f));
            if(!right)
                Instantiate(bulletPrefab, transform.position + new Vector3(-1f, 0.8f, 0), Quaternion.Euler(0f, 0f, -90f));
            canShoot = false;
            StartCoroutine(waitForShoot());
        }
    }

    IEnumerator waitForShoot()
    {
        yield return new WaitForSeconds(3f);
        canShoot = true;
    }

    IEnumerator WaitToTurn()
    {
        yield return new WaitForSeconds(3f);
        canMove = true;
        if (transform.position.x == pointA.position.x)
        {
            _currentTarget = pointB.position;
            right = true;
        }
        else if (transform.position.x == pointB.position.x)
        {
            _currentTarget = pointA.position;
            right = false;
        }
    }

    public void MoveAnimation(bool move)
    {
        _anim.SetBool("canMove", move);
    }
}
