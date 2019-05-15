using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool resetJump = false;
    [SerializeField] private GameObject paintDrop;
    [SerializeField] private float fireRate = 0.25f;
    private float canFire = 0f;
    public bool havebrush = false;
    private bool rigth = true;
    public bool haveKey = true;
    public bool canMove = true;
    
    private SpriteRenderer sprite;
    private Animator anim;
    private Rigidbody2D _rigid;

    [SerializeField] private Animator transitionAnim;
    [SerializeField] private string sceneName;
    

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Movement();
        }
        
        Attack();
    }

    private void Movement()
    {
        //move the player
        float move = Input.GetAxisRaw("Horizontal") * _speed;
        if (move > 0)
        {
            sprite.flipX = false;
            rigth = true;
        }else if(move < 0)
        {
            sprite.flipX = true;
            rigth = false;
        }
        transform.Translate(Vector3.right * move * _speed * Time.deltaTime);
        MoveAnimation(move);
        
        //make the player jump
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            isGrounded = false;
            resetJump = true;
            StartCoroutine(ResetJumpCorutine());
            Jump(true);
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.8f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down * 1.8f, Color.green);

        if(hitInfo.collider != null && !resetJump)
        {
            Jump(false);
            isGrounded = true;
        }

        RaycastHit2D dangerHit = Physics2D.Raycast(transform.position, Vector2.down, 1.8f, 1 << 9);

        if (dangerHit.collider != null)
        {
            StartCoroutine(LoadScene());
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            shoot();
        }
    }

    private void shoot()
    {
        if (havebrush)
        {
            if (Time.time > canFire)
            {
                PincelAttackAnim(true);
                if(rigth)
                    Instantiate(paintDrop, transform.position + new Vector3(2.42f, 0.89f, 0), Quaternion.Euler(0, 0, 90f));
                if(!rigth)
                    Instantiate(paintDrop, transform.position + new Vector3(-2.42f, 0.89f, 0), Quaternion.Euler(0, 0, -90f));
                canFire = Time.time + fireRate;
            }

            StartCoroutine(WaitForShoot());
        }
    }

    IEnumerator WaitForShoot()
    {
        yield return new WaitForSeconds(0.5f);
        PincelAttackAnim(false);
    }

    IEnumerator ResetJumpCorutine()
    {
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }

    public void MoveAnimation(float move)
    {
        anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jump)
    {
        anim.SetBool("Jump", jump);
    }

    public void PincelAttackAnim(bool attack)
    {
        anim.SetBool("Attack", attack);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
        Destroy(this.gameObject);
    }
}
