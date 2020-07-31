using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //For animation
    private Animator anim;
    private SpriteRenderer spr;
    private BoxCollider2D boxc;
    private PlayerController pc;
    private PlayerAttack pa;
    public bool playerDead;
    public bool isHurt;

    //For giving velocity
    [SerializeField] private float knockbackX;
    [SerializeField] private float knockbackY;
    [SerializeField] private float negKnocbackX;
    public float speed = 0f;
    [SerializeField] private float jumpForce = 0f;
    private Rigidbody2D rb;
    private float moveInput = 0f;
    private bool isRunning;
    private Transform ec;
    private bool isWalking;
    private bool faceRight;
    private int jumpCounter;

    //Ground Detection
    private bool isGrounded;
    [SerializeField] Transform ground;
    [SerializeField] private float radius = 0f;
    [SerializeField] private LayerMask thatGround;

    private SwordPowerUp spu;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        boxc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        pa = GetComponent<PlayerAttack>();
        isRunning = false;
        playerDead = false;
        ec = GameObject.FindGameObjectWithTag("MyEnemy").transform;
        spu = GameObject.FindGameObjectWithTag("PowerUpTag").GetComponent<SwordPowerUp>();
    }

    // Update is called once per frame
    void Update()
    {
        
        /*//Walking
        if (Input.GetButton("Horizontal") && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("IsWalking", true);
        }
        else
            anim.SetBool("IsWalking", false);*/

        PlayerJump();

        //Rotate player accordingly
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            faceRight = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            faceRight = true;
        }
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        //detects floor
        isGrounded = Physics2D.OverlapCircle(ground.position, radius, thatGround);

        if (playerDead)
        {
            rb.velocity = new Vector2(0f, 0f);
        }


        if (!Input.GetKey(KeyCode.LeftShift) && Input.GetButton("Horizontal") && faceRight)
        {
            PlayerWalk(new Vector2(Input.GetAxis("Horizontal"), 0f));
            speed = 5f;
            anim.SetFloat("IsRunning", -0.1f);
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && Input.GetButton("Horizontal") && !faceRight)
        {
            PlayerWalk(new Vector2(-(Input.GetAxis("Horizontal")), 0f));
            speed = 5f;
            anim.SetFloat("IsRunning", -0.1f);
        }
        else if ((Input.GetKey(KeyCode.LeftShift) && Input.GetButton("Horizontal")) && !faceRight)
        {
            PlayerRun(new Vector2(-Input.GetAxis("Horizontal"), 0f));
            speed = 10f;
            anim.SetFloat("IsWalking", -0.1f);
        }
        else if ((Input.GetKey(KeyCode.LeftShift) && Input.GetButton("Horizontal")) && faceRight)
        {
            PlayerRun(new Vector2(Input.GetAxis("Horizontal"), 0f));
            speed = 10f;
            anim.SetFloat("IsWalking", -0.1f);
        }
        else
        {
            PlayerIdle();
            anim.SetFloat("IsWalking", -0.1f);
            anim.SetFloat("IsRunning", -0.1f);
        }

    }

    public void PlayerIdle()
    {
        isWalking = false;
        anim.SetBool("IsIdle", true);
    }

    private void PlayerWalk(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
        anim.SetFloat("IsWalking", 1f);
        anim.SetBool("IsIdle", false);
    }

    private void PlayerRun(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
        anim.SetFloat("IsRunning", 1f);
        anim.SetBool("IsIdle", false);
    }

    public void PlayerJump()
    {
        //Jumping
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("IsAttacking") && jumpCounter > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && !SwordPowerUp.powerUp)
            {
                anim.SetTrigger("IsJumping");
                rb.velocity = Vector2.up * jumpForce;
                anim.SetBool("IsGrounded", false);
                isGrounded = false;
                jumpCounter--;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && SwordPowerUp.powerUp)
            {
                jumpForce = 40f;
                anim.SetTrigger("IsJumping");
                rb.velocity = Vector2.up * jumpForce;
                anim.SetBool("IsGrounded", false);
                isGrounded = false;
                jumpCounter--;
            }
            else if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("IsJumping"))
            {
                jumpForce = 25f;
                anim.SetBool("IsGrounded", true);
                isGrounded = true;
            }
        }
    }

    public void EnterPortal()
    {
        Input.GetButtonDown("Vertical");
    }

    public void PlayerHurt()
    {
        isHurt = true;
        //anim.Play("KnightHurt");
    }

    //Called in HealthSystem
    public void PlayerDeath()
    {
        anim.Play("KnightDeath");
        rb.velocity = new Vector2(0, 0);
        pc.enabled = false;
        pa.enabled = false;
        playerDead = true;
        isHurt = false;
    }

    public void GetKnockback()
    {
        if (transform.position.x <= ec.position.x)
        {
            rb.velocity = new Vector2(negKnocbackX, knockbackY);
            Invoke("ResetPlayerVelocity", 0.5f);
        }
        else if (transform.position.x >= ec.position.x)
        {
            rb.velocity = new Vector2(knockbackX, knockbackY);
            Invoke("ResetPlayerVelocity", 0.5f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            isGrounded = true;
            jumpCounter = 1;
        }
    }

    public void ResetPlayerVelocity()
    {
        rb.velocity = new Vector2(0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ground.position, radius);
    }
}
