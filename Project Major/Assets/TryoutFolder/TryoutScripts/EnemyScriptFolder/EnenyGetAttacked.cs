using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenyGetAttacked : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float knockbackX;
    [SerializeField] private float knockbackY;
    [SerializeField] private Transform player;
    public bool isNear;

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
    }

    public void GotHurt()
    {
        if (isNear == true)
        {
            anim.SetTrigger("IsHurt");
        }
    }

    public void GetKnockback()
    {
        if (isNear == true)
        {
            if (transform.position.x > player.position.x)
                rb.velocity = new Vector2(knockbackX, knockbackY);
            else
                rb.velocity = new Vector2(-knockbackX, knockbackY);
        }

    }
}
