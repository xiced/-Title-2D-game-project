using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //For animation
    [SerializeField] private Animator anim;
    private SpriteRenderer spr;
    private BoxCollider2D boxc;
    private PlayerController pc;
    private PlayerAttack pa;
    public bool playerDead;

    //For giving velocity
    [SerializeField] private float knockbackX;
    [SerializeField] private float knockbackY;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float jumpForce = 0f;
    private Rigidbody2D rb;
    private float moveInput = 0f;
    private bool isRunning;
    [SerializeField] private Transform ec;
    

    //Ground Detection
    private bool isGrounded;
    [SerializeField] Transform ground;
    [SerializeField] private float radius = 0f;
    [SerializeField] private LayerMask thatGround;
    

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
    }

    // Update is called once per frame
    void Update()
    {
        //Walking
        if (Input.GetButton("Horizontal") && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("IsWalking", true);
        }
        else
            anim.SetBool("IsWalking", false);

        PlayerJump();

        //Rotate player accordingly
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("IsAttacking") && PauseMenuScript.PausedGame == false && !isRunning)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        else if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("IsAttacking"))
        {
            rb.velocity = new Vector2(0, 0);
        }

        PlayerRun();

        //detects floor
        isGrounded = Physics2D.OverlapCircle(ground.position, radius, thatGround);
    }

    public void PlayerRun()
    {
        //Running
        if (((Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift))) || (Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift)))
        {
            anim.SetBool("IsRunning", true);
            speed = 10f;
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        else
            anim.SetBool("IsRunning", false);
    }

    public void PlayerJump()
    {
        //Jumping
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("IsAttacking"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            {
                anim.SetTrigger("IsJumping");
                rb.velocity = Vector2.up * jumpForce;
            }
        }
    }

    public void EnterPortal()
    {
        Input.GetButtonDown("Vertical");
    }

    public void PlayerHurt()
    {
        anim.Play("KnightHurt");
    }

    //Called in HealthSystem
    public void PlayerDeath()
    {
        anim.Play("KnightDeath");
        rb.velocity = new Vector2(0, 0);
        pc.enabled = false;
        pa.enabled = false;
        playerDead = true;
    }

    public void GetKnockback()
    {
            if (transform.position.x < ec.position.x)
                rb.velocity = new Vector2(knockbackX, knockbackY);
            else
                rb.velocity = new Vector2(-knockbackX, knockbackY);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ground.position, radius);
    }
}
