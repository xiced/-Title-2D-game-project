using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleVictory : MonoBehaviour
{
    [SerializeField] private PortalScript ps;
    [SerializeField] private float initposX;
    [SerializeField] private float initposY;
    [SerializeField] private float initposZ;
    [SerializeField] private float inittime;
    [SerializeField] private float exitPosX;
    [SerializeField] private float exitPosY;
    [SerializeField] private float exitPosZ;
    [SerializeField] private float exitTime;

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Portal").GetComponent<PortalScript>();
    }
    public void ScaleVictoryText()
    {
        if (ps.enterPortal == true)
        {
            LeanTween.scale(gameObject, new Vector3(exitPosX, exitPosY, exitPosZ), exitTime);
        }
    }

}
