using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private float speed = 5f;
    private Vector3 Velocity = Vector3.zero;
    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
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

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);

        }
    }

}
