using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private GameManager code;

    //For movement
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private float speed = 5f;
    private Vector3 Velocity = Vector3.zero;
    private Rigidbody2D myRigidbody2D;
    private float moveInput;
    private bool lookright = true;
    

    // Start is called before the first frame update
    void Start()
    {
        code = GameObject.Find("gameManager").GetComponent<GameManager>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is better with physics
    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        myRigidbody2D.velocity = new Vector2(moveInput * speed, myRigidbody2D.velocity.y);
        if(lookright == false && moveInput > 0)
        {
            ChangeDirection();
        }
        else if(lookright == true && moveInput < 0)
        {
            ChangeDirection();
        }
            
    }

    void Update()
    {
        Jump();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);

        }
    }

    void ChangeDirection()
    {
        lookright = false;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }



    //old movement with sliding bug
    /*void Movement()
    {
        //Check if player move right
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            myRigidbody2D.velocity = new Vector2(speed, myRigidbody2D.velocity.y);
        }
        //Check if player move left
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            myRigidbody2D.velocity = new Vector2(-speed, myRigidbody2D.velocity.y);
        }
        //else
        //{
        //    myRigidbody2D.velocity = new Vector2(0, myRigidbody2D.velocity.y);
        //}
    }
    */


}
