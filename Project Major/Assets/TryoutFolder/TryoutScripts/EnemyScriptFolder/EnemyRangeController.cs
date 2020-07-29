using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeController : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float stopDistance = 0f;
    [SerializeField] private Transform player;
    [SerializeField] private float viewRange = 0f;
    [SerializeField] private float distanceFromPlayer = 0f;
    [SerializeField] private float rayLen = 0f;
    [SerializeField] private Transform groundDetect;
    [SerializeField] private Transform wallDetect;
    [SerializeField] private float wallRayLen = 0f;
    private EnemyHealth eh;
    public bool isAlive;
    private Animator anim;
    private Rigidbody2D rb;
    private GameManager code;
    private bool movingLeft = true;
    public float health;
    private int layerMask = 1 << 9;
    private int count;
    private ThrowWeapon tw;
    public bool isAttack;

    private float timeToAttack;
    [SerializeField] private float startAttackTime;
    [SerializeField] private Transform cirPos;
    [SerializeField] private float range;
    [SerializeField] private LayerMask thePlayer;
    [SerializeField] private float damageValue = 1f;

    private void Start()
    {
        code = GameObject.Find("gameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Patrolling();
        isAlive = true;
        layerMask = ~layerMask;
        count = 1;
        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>(), true);
        tw = GetComponent<ThrowWeapon>();
        isAttack = false;
        eh = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (eh.health <= 0)
        {
            Death();
            if (count > 0)
            {
                count--;
                Invoke("DeathAnimation", 0.5f);
            }
        }

        if (!isAlive)
        {

            Destroy(gameObject, 10f);
        }

        distanceFromPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceFromPlayer < viewRange)
        {
            MoveTowardPlayer();
        }
        else
        {
            Patrolling();
        }
    }

/*    private void EnemyAttackHB()
    {
        //Collides with everything inside the circle 
        Collider2D[] enemyAOE = Physics2D.OverlapCircleAll(cirPos.position, range, thePlayer);
        for (int i = 0; i < enemyAOE.Length; i++)
        {
            enemyAOE[i].GetComponent<PlayerHealth>().GetDamaged();
        }
    }*/

    //Play the animation once instead of looping
    private void DeathAnimation()
    {
        anim.SetTrigger("IsDead");
    }

    private void Patrolling()
    {
        if (isAlive)
        {
            anim.SetBool("IsWalking", true);
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            //verifies if there's a platform infront
            RaycastHit2D groundAhead = Physics2D.Raycast(groundDetect.position, Vector2.down, rayLen);
            if (groundAhead.collider == false)
            {
                Flip();
            }

            //verifies if there's a wall infront
            RaycastHit2D wallAhead = Physics2D.Raycast(wallDetect.position, transform.position, wallRayLen, layerMask);
            if (wallAhead.collider == true)
            {
                Flip();
            }
        }
    }

    private void MoveTowardPlayer()
    {
        //switch state from moving towards and attacking
        if ((Vector2.Distance(transform.position, player.position) > stopDistance) && isAlive)
        {
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsAttacking", false);

            //if the player is on the right side of the enemy
            if (transform.position.x > player.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
        }
        else if ((Vector2.Distance(transform.position, player.position) <= stopDistance) && isAlive)
        {
            if (transform.position.x > player.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            transform.position = this.transform.position;
            anim.SetBool("IsWalking", false);
            tw.isAttack = true;
            isAttack = true;
            if (timeToAttack <= 0)
            {
                //Invoke("EnemyAttackHB", 1f);
                timeToAttack = startAttackTime;
            }
            else
                timeToAttack -= Time.deltaTime;
        }
    }

    //Rotate the enemy accordingly on position x
    private void Flip()
    {
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

    public void Death()
    {
        if (isAlive)
        {
            code.AddPoints();
        }
        gameObject.layer = 13;
        isAlive = false;
        transform.position = this.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(cirPos.position, range);
    }
}
