using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformUpward : MonoBehaviour
{
    [SerializeField] private Transform initPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private Transform newPos;
    [SerializeField] private float speed;
    private Vector2 tmpPos;

    private void Start()
    {
        tmpPos = initPos.position;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, tmpPos, speed * Time.deltaTime);

        if (transform.position == newPos.position)
        {
            tmpPos = endPos.position;
        }
        else if (transform.position == endPos.position)
        {
            tmpPos = newPos.position;
        }
    }

}
