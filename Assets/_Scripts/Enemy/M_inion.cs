using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_inion : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform rigthP, leftP;
    [SerializeField] private float speed = 2.0f;

    private Vector3 _currentTarget;
    private BulletEnemy bullet;
    private SpriteRenderer _sprite;
    private Animator _anim;

    private bool canShoot = true;
    private bool right = false;
    private bool canMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        bullet = bulletPrefab.GetComponent<BulletEnemy>();
        _currentTarget = rigthP.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }

    void Movement()
    {
        if (_currentTarget == rigthP.position)
        {
            _sprite.flipX = true;
        }
        else
        {
            _sprite.flipX = false;
        }

        if ((transform.position.x == rigthP.position.x) || (transform.position.x == leftP.position.x))
        {
            MoveAnimation(false);
            canMove = false;
            StartCoroutine(waitToWalk());
        }

        if (canMove)
        {
            MoveAnimation(true);
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        if (canShoot)
        {   
            if (_currentTarget == leftP.position)
                Instantiate(bulletPrefab, transform.position + new Vector3(1.2f, 0.8f, 0), Quaternion.Euler(0f, 0f, 40f));
            if (_currentTarget == rigthP.position)
                Instantiate(bulletPrefab, transform.position + new Vector3(-1.2f, 0.8f, 0), Quaternion.Euler(0f, 0f, -40f));
            canShoot = false;
            StartCoroutine(waitForShoot());
        }
    }

    IEnumerator waitForShoot()
    {
        yield return new WaitForSeconds(3f);
        canShoot = true;
    }

    IEnumerator waitToWalk()
    {
        yield return new WaitForSeconds(2f);
        canMove = true;
        if (transform.position.x == rigthP.position.x)
        {
            _currentTarget = leftP.position;
            bullet.right = true;
        }
        else if (transform.position.x == leftP.position.x)
        {
            _currentTarget = rigthP.position;
            bullet.right = false;
        }
    }

    public void MoveAnimation(bool move)
    {
        _anim.SetBool("canMove", move);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
