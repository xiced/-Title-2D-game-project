using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenyGetAttacked : MonoBehaviour
{

    /* --------------------- OBSOLETE, FOR REFERENCE ONLY ------------------- */

    public static Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float knockbackX;
    [SerializeField] private float knockbackY;
    [SerializeField] private Transform player;
    public bool isNear;
    private float delay;
    private float initDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isNear = false;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= 8)
        {
            isNear = true;
        }
        else
            isNear = false;


        delay -= Time.deltaTime;
        if(delay <= 0)
        {
            rb.isKinematic = true;
            delay = initDelay;
        }
    }

    /*public void GotHurt()
    {
        if (isNear == true)
        {
            anim.SetTrigger("IsHurt");
        }
    }*/

    public void GetKnockback()
    {
        if (isNear == true)
        {
            if (transform.position.x > player.position.x)
            {
                rb.velocity = new Vector2(knockbackX, knockbackY);
                rb.isKinematic = false;
            }
            else
            {
                rb.velocity = new Vector2(-knockbackX, knockbackY);
                rb.isKinematic = false;
            }
        }

    }
}
