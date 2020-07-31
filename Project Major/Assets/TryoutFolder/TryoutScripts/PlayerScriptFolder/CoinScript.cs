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
        //Physics2D.IgnoreLayerCollision(9, 11);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            code.AddPoints();
            Destroy(gameObject);
        }
    }
}
