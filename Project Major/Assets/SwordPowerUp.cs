using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPowerUp : MonoBehaviour
{
    private EnemyHealth eh;
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        eh = GameObject.FindGameObjectWithTag("MyEnemy").GetComponent<EnemyHealth>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Collided with Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
