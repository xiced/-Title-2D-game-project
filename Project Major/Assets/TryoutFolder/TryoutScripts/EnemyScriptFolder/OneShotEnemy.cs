using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotEnemy : MonoBehaviour
{
    private EnemyScript es;

    // Start is called before the first frame update
    void Start()
    {
        es = GameObject.Find("Golem").GetComponent<EnemyScript>();
    }

    public void OneShot()
    {
        es.EnemyDeath();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OneShot();
        }
    }
}
