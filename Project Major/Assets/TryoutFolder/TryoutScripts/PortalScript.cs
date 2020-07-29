using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private LayerMask thePlayer;
    [SerializeField] private Transform boxPosition;
    [SerializeField] private float range;
    private PlayerController pc;
    public bool enterPortal;

    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enterPortal = false;
    }

    void Update()
    {
        Collider2D[] cc2d = Physics2D.OverlapCircleAll(boxPosition.position, range, thePlayer);

        for (int i = 0; i < cc2d.Length; i++)
        {
            if (Input.GetButtonDown("Vertical") && PauseMenuScript.PausedGame == false)
            {
                pc.Invoke("EnterPortal", 0.1f);
                HasEntered();
                Debug.Log("In Portal");
            }
        }
    }

    public void HasEntered()
    {
        enterPortal = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(boxPosition.position, range);
    }

}
