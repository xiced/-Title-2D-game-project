using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPowerUp : MonoBehaviour
{
    public static bool powerUp;
    private SpriteRenderer spr;
    private BoxCollider2D bc;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        powerUp = false;
        spr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Collided with Player");
            powerUp = true;
            bc.enabled = false;
            spr.enabled = false;
            Destroy(rb);
        }
    }

    public void IsPowered()
    {
        powerUp = true;
    }

    public void LosePower()
    {
        powerUp = false;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUp == true)
        {
            Invoke("LosePower", 10f);
        }
    }
}
