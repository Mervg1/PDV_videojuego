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
    [SerializeField] private GameObject paintDrop, paintDrop2, paintDrop3;
    [SerializeField] public GameObject weapon1, weapon2;
    [SerializeField] private float fireRate = 0.25f;

    public string level = "";
    private float canFire = 0f;
    public bool havebrush = false;
    private bool rigth = true;
    public bool haveKey, havePistol, havePaint, haveGreen;
    public bool canMove = true;
    private bool brushOrGun = false;
    
    private SpriteRenderer sprite;
    private Animator anim;
    private Rigidbody2D _rigid;


    [SerializeField] private Animator transitionAnim;
    [SerializeField] private string sceneName;

    private GameMaster gm;


    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        haveKey = false;
        havePistol = false;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
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

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.9f, 1 << 8);
        RaycastHit2D hitInfoRigth = Physics2D.Raycast(new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z), Vector2.down, 1.9f, 1 << 8);
        RaycastHit2D hitInfoLeft = Physics2D.Raycast(new Vector3(transform.position.x + -0.3f, transform.position.y, transform.position.z), Vector2.down, 1.9f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down * 1.9f, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z), Vector2.down * 1.9f, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x + -0.3f, transform.position.y, transform.position.z), Vector2.down * 1.9f, Color.green);

        if ((hitInfo.collider != null || hitInfoRigth.collider != null || hitInfoLeft.collider != null) && !resetJump)
        {
            Jump(false);
            isGrounded = true;
        }

        RaycastHit2D dangerHit = Physics2D.Raycast(transform.position, Vector2.down, 1.8f, 1 << 9);

        if (dangerHit.collider != null)
        {
            //StartCoroutine(LoadScene());
            Health.health -= 1;
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(brushOrGun);
            if (brushOrGun)
            {
                brushOrGun = false;
                weapon1.SetActive(true);
                weapon2.SetActive(false);

            }
            else
            {
                brushOrGun = true;
                weapon1.SetActive(false);
                weapon2.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            shoot();
        }
    }

    private void shoot()
    {
        if (havebrush == true && brushOrGun == false)
        {
            if (Time.time > canFire)
            {
                PincelAttackAnim(true);
                if (rigth)
                    Instantiate(paintDrop, transform.position + new Vector3(2.42f, 0.89f, 0), Quaternion.Euler(0, 0, 90f));
                if (!rigth)
                    Instantiate(paintDrop, transform.position + new Vector3(-2.42f, 0.89f, 0), Quaternion.Euler(0, 0, -90f));
                canFire = Time.time + fireRate;
            }

            StartCoroutine(WaitForShoot());
        }
        else if (havePistol == true && brushOrGun == true)
        {
            if (Time.time > canFire)
            {
                PincelAttackAnim(true);
                if (rigth == true)
                {
                    Instantiate(paintDrop, transform.position + new Vector3(2.42f, 0.89f, 0), Quaternion.Euler(0, 0, 120f));
                    Instantiate(paintDrop2, transform.position + new Vector3(2.42f, 0.89f, 0), Quaternion.Euler(0, 0, 90f));
                    Instantiate(paintDrop3, transform.position + new Vector3(2.42f, 0.89f, 0), Quaternion.Euler(0, 0, 60f));
                }

                if (rigth == false)
                {
                    Instantiate(paintDrop, transform.position + new Vector3(-2.42f, 0.89f, 0), Quaternion.Euler(0, 0, -120f));
                    Instantiate(paintDrop2, transform.position + new Vector3(-2.42f, 0.89f, 0), Quaternion.Euler(0, 0, -90f));
                    Instantiate(paintDrop3, transform.position + new Vector3(-2.42f, 0.89f, 0), Quaternion.Euler(0, 0, -60f));
                }

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
            Health.health -= 1;
        }
    }

    public void SavePLayer()
    {
        SaveSystem.SavePlayer(this);
    }
}
