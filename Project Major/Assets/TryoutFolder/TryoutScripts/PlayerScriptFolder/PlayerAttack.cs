using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform cirPos;
    [SerializeField] private float range;
    [SerializeField] private LayerMask theEnemy;
    [SerializeField] private int damageValue;
    [SerializeField] private AudioSource swing;
    [SerializeField]private float startSfx;
    private float timeToPlaySfx;
    private Animator anim;
    private EnemyController ec;
    private EnemyRangeController erc;
    private EnenyGetAttacked ega;
    private SwordPowerUp spu;
    [SerializeField]private PlayerController pc;
    private DinoBossScript dino;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ec = GameObject.FindGameObjectWithTag("MyEnemy").GetComponent<EnemyController>();
        swing = GetComponent<AudioSource>();
        erc = GameObject.FindGameObjectWithTag("MyEnemy").GetComponent<EnemyRangeController>();
        ega = GameObject.FindGameObjectWithTag("MyEnemy").GetComponent<EnenyGetAttacked>();
        spu = GameObject.FindGameObjectWithTag("PowerUpTag").GetComponent<SwordPowerUp>();
        dino = GameObject.FindGameObjectWithTag("MyEnemy").GetComponent<DinoBossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToPlaySfx -= Time.deltaTime;
        //Attacking
        if (Input.GetButtonDown("Attack") && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("IsJumping") && !SwordPowerUp.powerUp)
        {
            PlaySwingSfx();
            anim.SetTrigger("IsAttacking");
            AttackHitBox();
        }
        else if (Input.GetButtonDown("Attack") && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("IsJumping") && SwordPowerUp.powerUp)
        {
            anim.SetFloat("AttackSpeed", 3f);
            PlaySwingSfx();
            anim.SetTrigger("IsAttacking");
            AttackHitBox();
        }
        else if(SwordPowerUp.powerUp)
            anim.SetFloat("AttackSpeed", 1f);
    }

    private void PlaySwingSfx()
    {
        if (timeToPlaySfx <= 0)
        {
            swing.Play();
            timeToPlaySfx = startSfx;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(cirPos.position, range);
    }

    private void AttackHitBox()
    {
        Collider2D[] aoe = Physics2D.OverlapCircleAll(cirPos.position, range, theEnemy);

        for (int i = 0; i < aoe.Length; i++)
        {
            aoe[i].GetComponent<EnemyHealth>().GetDamaged(damageValue);
            aoe[i].GetComponent<DinoBossScript>().GetDamaged(damageValue);
        }
    }
}
