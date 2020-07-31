using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteorScript : MonoBehaviour
{
    [SerializeField] private GameObject meteor;
    [SerializeField] private Transform startPos;
    [SerializeField] private float delay;
    [SerializeField] private float initDelay = 2;
    private float range;


    // Update is called once per frame
    void Update()
    {
        range = Random.Range(1, 3);
        if (delay <= 0)
        {
            Invoke("SpawnMeteor", range);
            delay = initDelay;
        }
        else if (delay >= 0)
        {
            delay -= Time.deltaTime;
        }
    }

    void SpawnMeteor()
    {
        GameObject newMeteor = Instantiate(meteor, startPos);
        newMeteor.GetComponent<Rigidbody2D>().velocity = transform.right * Random.Range(-10f, -2f);
        Destroy(newMeteor, 5f);
    }
}
