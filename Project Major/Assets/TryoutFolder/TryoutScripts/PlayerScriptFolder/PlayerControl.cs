using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] Transform ground;
    [SerializeField] private float radius = 0f;
    [SerializeField] private LayerMask thatGround;
    private Rigidbody2D rb;
    [SerializeField] private SwordPowerUp spu;
    private Animator anim;
    private bool faceRight;
    private bool isGrounded;
    private float delay;
    private float initDelay = 1f;
    


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
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

        isGrounded = Physics2D.OverlapCircle(ground.position, radius, thatGround);

        PlayerJump();
        Turn();
        delay -= Time.deltaTime;
        if(delay <= 0 && isGrounded == true)
        {
            rb.isKinematic = true;
            anim.SetBool("IsGrounded", true);
            isGrounded = true;
        }
    }

   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer != LayerMask.NameToLayer("Environment"))
        {
            rb.isKinematic = false;
            rb.gravityScale = 5f;
            isGrounded = false;
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            isGrounded = true;
        }
    }*/

    private void PlayerIdle()
    {
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

    private void PlayerJump()
    {
        //Jumping
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("IsAttacking"))
        {
            if (Input.GetButton("Jump") && isGrounded == true && !SwordPowerUp.powerUp)
            {
                rb.isKinematic = false;
                rb.gravityScale = 5f;
                anim.SetTrigger("IsJumping");
                rb.velocity = Vector2.up * jumpForce;
                anim.SetBool("IsGrounded", false);
                isGrounded = false;
                delay = initDelay;
            }
            else if (Input.GetButton("Jump") && isGrounded == true && SwordPowerUp.powerUp)
            {
                rb.isKinematic = false;
                rb.gravityScale = 5f;
                jumpForce = 40f;
                anim.SetTrigger("IsJumping");
                rb.velocity = Vector2.up * jumpForce;
                anim.SetBool("IsGrounded", false);
                isGrounded = false;
                delay = initDelay;
            }
            if(delay <= 0)
            {
                anim.ResetTrigger("IsJumping");
            }
        }
    }

    private void Turn()
    {
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

    //----------------FOR REFERENCES IF NEEDED ----------------------------

    /*public void PlayerWalk()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("IsAttacking") && PauseMenuScript.PausedGame == false && !isRunning)
            {
                moveInput = Input.GetAxisRaw("Horizontal");
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
                isWalking = true;
            }
            else if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("IsAttacking"))
            {
                rb.velocity = new Vector2(0, 0);
                isWalking = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }*/

    /*public void PlayerRun()
    {
        //Running
        if (((Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift))) && !spu.powerUp)
        {
            anim.SetBool("IsRunning", true);
            speed = 10f;
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        else if (((Input.GetButton("Horizontal") && !Input.GetKey(KeyCode.LeftShift))) && !spu.powerUp)
        {
            anim.SetBool("IsRunning", false);
            speed = 5f;
        }
        else if (((Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift))) && spu.powerUp)
        {
            anim.SetBool("IsRunning", true);
            speed = 35f;
        }
        else if (((Input.GetButton("Horizontal") && !Input.GetKey(KeyCode.LeftShift))) && spu.powerUp)
        {
            anim.SetBool("IsRunning", false);
            speed = 20f;
        }
        else
            anim.SetBool("IsRunning", false);
    }*/
}
