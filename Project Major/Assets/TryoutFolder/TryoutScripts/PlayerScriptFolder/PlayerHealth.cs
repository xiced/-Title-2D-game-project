using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameManager code;
    private PlayerController pc;
    private EnemyController ec;
    private EnemyRangeController erc;

    [SerializeField] private int maxHealth = 0;
    public int currentHealth;
    [SerializeField] private int getDamaged = 1;

    // Start is called before the first frame update
    void Start()
    {
        code = GameObject.Find("gameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
        pc = GameObject.Find("Knight").GetComponent<PlayerController>();
        erc = GameObject.FindGameObjectWithTag("MyEnemy").GetComponent<EnemyRangeController>();
        ec = GameObject.FindGameObjectWithTag("MyEnemy").GetComponent<EnemyController>();
    }

    public void GetDamaged()
    {
        currentHealth -= getDamaged;
        if (currentHealth <= 0 && gameObject.tag == "Player")
        {
            //pc.PlayerDeath();
        }
        if(currentHealth >= 0 && gameObject.tag == "Player")
        {
            //pc.PlayerHurt();
            //pc.GetKnockback();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("FlyingWeapon"))
        {
            code.LosePoints();
            //GetDamaged();
        }
    }
}
