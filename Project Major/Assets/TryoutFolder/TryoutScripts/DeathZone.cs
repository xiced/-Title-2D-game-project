using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private PlayerController pc;
    [SerializeField] private SpriteRenderer player;
    [SerializeField] private Rigidbody2D rb;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            pc.playerDead = true;
            player.enabled = false;
            rb.velocity = new Vector2(0f, 0f);
        }
    }

}
