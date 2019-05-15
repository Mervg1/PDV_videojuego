using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;

    protected Vector3 _currentTarget;
    protected SpriteRenderer _sprite;
    protected Animator _anim;
    protected Player player;
    protected bool isHit = false;

    public virtual void Init()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && _anim.GetBool("Combat") == false)
        {
            return;
        }
        Movement();
    }

    public virtual void Movement()
    {
        if(_currentTarget == pointA.position)
        {
            _sprite.flipX = true;
        }
        else
        {
            _sprite.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            _currentTarget = pointB.position;
            _anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            _currentTarget = pointA.position;
            _anim.SetTrigger("Idle");
        }

        if(isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if (distance > 2.0f)
        {
            isHit = false;
            _anim.SetBool("Combat", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
