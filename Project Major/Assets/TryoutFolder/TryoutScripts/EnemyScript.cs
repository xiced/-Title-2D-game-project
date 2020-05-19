using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float rayLen = 0f;
    private bool movingLeft = true;

    [SerializeField] private Transform groundDetect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        //raycast detects the floor
        RaycastHit2D groundAhead = Physics2D.Raycast(groundDetect.position, Vector2.down, rayLen);

        //turn enemy if no floor detected
        if(groundAhead.collider == false)
        {
            //turn right
            if(movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = false;
            }
            else //turn left
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = true;
            }
        }
    }
}
