using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    [SerializeField] private GameObject myWeapon;
    [SerializeField] private float velocityWeap;
    [SerializeField] private Transform shootPos;
    [SerializeField] private float startAttackTime;
    [SerializeField] private float delay;
    [SerializeField] private Transform player;
    private EnemyRangeController erc;
    private Animator anim;
    private float timeToAttack;
    private int count;
    public bool isAttack;

    private void Start()
    {
        isAttack = false;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        erc = GetComponent<EnemyRangeController>();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("TimeAttacking", 2f);
    }

    void TimeAttacking()
    {
        if (isAttack && erc.isAlive)
        {
            if (isAttack == true && count <= 1 && timeToAttack <= 0)
            {
                anim.SetBool("IsIdle", false);
                anim.SetTrigger("IsAttacking");
                if (delay <= 0)
                {
                    GameObject newWeapon = Instantiate(myWeapon, shootPos.position, shootPos.rotation);
                    newWeapon.GetComponent<Rigidbody2D>().velocity = transform.right * -velocityWeap;
                    Destroy(newWeapon, 5f);
                }
                count++;
                delay = 2;
                timeToAttack = startAttackTime;
            }
            else if (count >= 0)
            {
                count--;
            }
            else
            {
                anim.ResetTrigger("IsAttacking");
                anim.SetBool("IsIdle", true);
                anim.SetBool("IsWalking", false);
                timeToAttack -= Time.deltaTime;
                delay -= Time.deltaTime;
            }
        }
    }
}
