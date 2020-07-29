using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private GameManager code;

    // Start is called before the first frame update
    void Start()
    {
        code = GameObject.Find("gameManager").GetComponent<GameManager>();;
        Physics2D.IgnoreLayerCollision(9, 11);
    }

    //destroy coin when player touch
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            code.AddPoints();
            Destroy(gameObject);
        }
    }
}
