using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushPushScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private float jumpForce;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.velocity = new Vector2(transform.position.x, transform.position.y + jumpForce);
        }
    }
}
