using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    //SCRIPT IS OBSOLETE------------------------------------------------------------------------------------------------------
    //THIS IS KEPT FOR REFERENCES



    [SerializeField] private float speed = 0f;
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 0f;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            anim.SetBool("IsAttacking", false);
        }
        else if (Vector2.Distance(transform.position, target.position) < distance)
        {
            anim.SetBool("IsAttacking", true);
        }
    }
}
