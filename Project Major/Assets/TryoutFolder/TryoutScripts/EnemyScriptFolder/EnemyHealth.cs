using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    [SerializeField] private int getDamaged;
    private SwordPowerUp spu;
    private Animator anim;
    [SerializeField] private Transform player;
    public bool isNear;
    private Rigidbody2D rb;
    [SerializeField] private float knockbackX;
    [SerializeField] private float knockbackY;
    private float delay;
    private float initDelay = 3f;

    private void Start()
    {
        spu = GameObject.FindGameObjectWithTag("PowerUpTag").GetComponent<SwordPowerUp>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        if (delay <= 0)
        {
            rb.isKinematic = true;
            rb.velocity = new Vector2(0f, 0f);
            delay = initDelay;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

    public void GetKnockback()
    {
        if (isNear == true)
        {
            if (transform.position.x > player.position.x)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.velocity = new Vector2(knockbackX, knockbackY);
                rb.isKinematic = false;
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.velocity = new Vector2(-knockbackX, knockbackY);
                rb.isKinematic = false;
            }
        }
    }

    public void GetDamaged(int damageValue)
    {
        anim.SetTrigger("IsHurt");
        GetKnockback();
        if (SwordPowerUp.powerUp == true)
        {
            health -= damageValue;
            health -= damageValue;
            health -= damageValue;
            health -= damageValue;
            health -= damageValue;
        }
        else
        {
            health -= damageValue;
        }
    }
}
