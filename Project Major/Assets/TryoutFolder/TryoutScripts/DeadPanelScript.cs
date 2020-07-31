using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPanelScript : MonoBehaviour
{
    [SerializeField] private float initposX;
    [SerializeField] private float initposY;
    [SerializeField] private float initposZ;
    [SerializeField] private float inittime;
    [SerializeField] private float exitPosX;
    [SerializeField] private float exitPosY;
    [SerializeField] private float exitPosZ;
    [SerializeField] private float exitTime;
    [SerializeField] private PlayerHealth ph;

    private void Start()
    {
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void ScaleDeadText()
    {
        if(ph.currentHealth <= 0)
        {
            LeanTween.scale(gameObject, new Vector3(exitPosX, exitPosY, exitPosZ), exitTime).setIgnoreTimeScale(true);
        }
    }
}
