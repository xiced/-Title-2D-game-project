using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShowerScript : MonoBehaviour
{
    private float range;
    private Rigidbody2D rb;
    private float tr;
    private float alpha;
    private float sizeX;
    private float sizeY;
    [SerializeField]private Transform meteor;

    private void Start()
    {
        alpha = Random.Range(0, 1);
        range = Random.Range(0, 3);
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>().startColor.a;
        sizeX = Random.Range(0.05f, 0.1f);
        sizeY = Random.Range(0.07f, 0.1f);
        meteor = GameObject.FindGameObjectWithTag("Meteor").transform;
        meteor.transform.localScale = new Vector3(sizeX, sizeY);
    }

    private void Update()
    {
        alpha = Random.Range(0, 1);
        tr = alpha;
        range = Random.Range(0.00f, 3f);
        rb.gravityScale = range;
    }

}
