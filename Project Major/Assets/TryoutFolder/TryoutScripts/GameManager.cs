using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //For scoring system
    [SerializeField] private Text score;
    private int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        //start at 0
        score.text = "Score: " + points.ToString("D3");
    }

    public void AddPoints()
    {
        points += 100;
        score.text = "Score: " + points.ToString("D3");
    }

    public void LosePoints()
    {
        points -= 25;
        score.text = "Score: " + points.ToString("D3");
    }
}
