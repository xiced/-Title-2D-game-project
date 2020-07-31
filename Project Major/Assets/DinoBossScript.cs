using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoBossScript : MonoBehaviour
{
    public int health;
    public int damage;
    public Slider healthbar;
    public int damagaValue;

    // Start is called before the first frame update
    void Start()
    {
        healthbar.value = health;
    }

    public void GetDamaged(int damaged)
    {
        if (SwordPowerUp.powerUp == true)
        {
            health -= damaged;
            health -= damaged;
            health -= damaged;
            health -= damaged;
            health -= damaged;
        }
        else
        {
            health -= damaged;
        }
    }
}
