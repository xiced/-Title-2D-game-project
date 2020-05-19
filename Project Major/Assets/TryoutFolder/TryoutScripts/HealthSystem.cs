using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private GameManager code;

    [SerializeField] private int maxHealth = 0;
    [SerializeField] private int currentHealth;
    [SerializeField] private int getDamaged = 1;

    // Start is called before the first frame update
    void Start()
    {
        code = GameObject.Find("gameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
    }

    public void GetDamaged()
    {
        currentHealth -= getDamaged;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            code.LosePoints();
            GetDamaged();
        }
        else if (collision.gameObject.tag == "Player")
        {
            GetDamaged();
        }
    }
}
