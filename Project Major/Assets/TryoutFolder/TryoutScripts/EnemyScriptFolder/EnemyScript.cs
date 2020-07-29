using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //SCRIPT IS OBSOLETE----------------------------------------------------------------------------------------
    //THIS IS KEPT FOR REFERENCES



    [SerializeField] private float speed = 0f;
    [SerializeField] private float rayLen = 0f;
    [SerializeField] private Transform groundDetect;
    [SerializeField] private Transform wallDetect;
    [SerializeField] private float maxHealth = 0;
    [SerializeField] private float currentHealth;
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 0f;
    [SerializeField] private bool knockback;
    [SerializeField] private float knockbackX;
    [SerializeField] private float knockbackY;

    private GameManager code;
    private bool movingLeft = true;
    private Animator anim;
    private Rigidbody2D rb;
    private EnemyScript es;
    private BoxCollider2D bc;
    private float disappearTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(9, 11);
        es = GetComponent<EnemyScript>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        code = GameObject.Find("gameManager").GetComponent<GameManager>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.left * speed * Time.deltaTime);
        //raycast detects the floor
        RaycastHit2D groundAhead = Physics2D.Raycast(groundDetect.position, Vector2.down, rayLen);
        //RaycastHit2D wallAhead = Physics2D.Raycast(wallDetect.position, Vector2.left, rayLen);

        //turn enemy if no floor detected
        if (groundAhead.collider == false)
        {
            //Debug.Log("walldetect");
            //turn right
            if (movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = false;
            }
            else //turn left
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = true;
            }
        }

        //Attack at certain distance
        if (Vector2.Distance(transform.position, target.position) < distance)
        {
            //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            anim.SetBool("IsAttacking", true);
            speed = 0f;
        }
        else if(Vector2.Distance(transform.position, target.position) > distance)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            anim.SetBool("IsAttacking", false);
            speed = 3f;
        }
    }

    public void GetDamaged(float damageValue)
    {
        currentHealth -= damageValue;
        Debug.Log("damage taken");
        if(currentHealth <= 0)
        {
            EnemyDeath();
            disappearTime -= Time.deltaTime;
            if(disappearTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rb.velocity = new Vector2(knockbackX, knockbackY);
        }
    }

    public void EnemyDeath()
    {
        anim.SetTrigger("IsDead");
        Physics2D.IgnoreLayerCollision(11, 12);
        code.AddPoints();
        bc.enabled = false;
        //if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("IsDead"))
        //{
        //    anim.enabled = false;
        //}
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        GetDamaged(damageValue);
    //    }
    //}
    
}

    
