using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float initposX;
    [SerializeField] private float initposY;
    [SerializeField] private float initposZ;
    [SerializeField] private float inittime;
    [SerializeField] private float exitPosX;
    [SerializeField] private float exitPosY;
    [SerializeField] private float exitPosZ;
    [SerializeField] private float exitTime;

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(initposX, initposY, initposZ), inittime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(exitPosX, exitPosY, exitPosZ), exitTime);
    }

}
