using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float range;
    [SerializeField] private LayerMask thePlayer;
    [SerializeField] private float angle;
    private PlayerHealth ph;
    private bool isHit;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
        {
            angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        Physics2D.IgnoreLayerCollision(14, 11);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isHit = true;
            ph.GetDamaged();
            Debug.Log("Hit player");
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            rb.isKinematic = true;
            isHit = false;
            rb.gravityScale = 0f;
            Debug.Log("Missed");
            Destroy(gameObject, 5f);
            transform.rotation = collision.gameObject.transform.rotation;
            transform.parent = collision.gameObject.transform;
            rb.velocity = Vector2.zero;
        }
    }

}
