using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    [SerializeField] private float knockbackX;
    [SerializeField] private float knockbackY;
    private Transform playerPos;
    private PlayerController rb;
    public bool isNear;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    private void Update()
    {
        PlayerLocation();
    }

    private void PlayerLocation()
    {
        if (Vector2.Distance(transform.position, playerPos.position) <= 8)
        {
            isNear = true;
        }
        else
            isNear = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("spike hit");
            //rb.GetKnockback();
        }
    }
}
