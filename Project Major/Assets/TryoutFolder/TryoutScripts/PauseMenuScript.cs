using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    //public so that it's accessible for other scripts
    public static bool PausedGame = false;
    [SerializeField] private GameObject pauseUI;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausedGame)
                Resume();
            else
                Pause();
        }

    }

    public void Resart()
    {
        Time.timeScale = 1f;
        Debug.Log("Restart");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("To Menu");
        //SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
        //disable canvas
        pauseUI.SetActive(false);

        //unfreeze game
        Time.timeScale = 1f;
        PausedGame = false;
    }

    void Pause()
    {
        //enabled canvas
        pauseUI.SetActive(true);

        //freeze game
        Time.timeScale = 0f;
        PausedGame = true;
    }

    
}
