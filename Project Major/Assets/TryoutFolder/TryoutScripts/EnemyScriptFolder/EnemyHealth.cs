using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    [SerializeField] private int getDamaged;

    public void GetDamaged(int damageValue)
    {
        health -= damageValue;
    }
}
