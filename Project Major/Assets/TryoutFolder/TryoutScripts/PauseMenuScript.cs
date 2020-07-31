using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    //public so that it's accessible for other scripts
    public static bool PausedGame = false;

    [SerializeField] private GameObject pauseUI;


    [SerializeField] private GameObject continueBtn;
    [SerializeField] private GameObject exitBtn;
    [SerializeField] private ScaleVictory victoryScaleTextScript;
    [SerializeField] private ScaleVictory continueScript;
    [SerializeField] private ScaleVictory exitScript;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject victoryText;


    [SerializeField] private GameObject defeatedUI;
    [SerializeField] private DeadPanelScript defeatPanelScript;
    [SerializeField] private DeadPanelScript defeatContinue;
    [SerializeField] private DeadPanelScript defeatExit;


    [SerializeField] private AudioSource cameraAudio;

    private PlayerController pc;
    private int count;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

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

        if (PortalScript.enterPortal == true)
        {
            victoryPanel.SetActive(true);
            victoryText.SetActive(true);
            victoryScaleTextScript.ScaleVictoryText();
            continueScript.ScaleVictoryText();
            exitScript.ScaleVictoryText();
        }
        else
            victoryPanel.SetActive(false);

        if(pc.playerDead == true)
        {
            defeatedUI.SetActive(true);
            defeatPanelScript.ScaleDeadText();
            defeatContinue.ScaleDeadText();
            defeatExit.ScaleDeadText();
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
        Time.timeScale = 1f;
        cameraAudio.volume = 0.3f;
        Debug.Log("Restart");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("To Menu");
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        PausedGame = false;
        cameraAudio.volume = 0.3f;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        PausedGame = true;
        cameraAudio.volume = 0.15f;
    }

    public void Continue()
    {
        if(count == 4)
        {
            SceneManager.LoadScene("MainMenu");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        count++;
        Debug.Log("Continued");
        Time.timeScale = 1f;
    }


}
