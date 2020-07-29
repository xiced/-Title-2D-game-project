using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    //public so that it's accessible for other scripts
    public static bool PausedGame = false;

    [SerializeField] private GameObject pauseUI;


    [SerializeField] private GameObject ctnbn;
    [SerializeField] private GameObject extbtn;
    [SerializeField] private ScaleVictory ctnScript;
    [SerializeField] private ScaleVictory extScript;

    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject vicText;
    [SerializeField] private ScaleVictory vstScript;

    [SerializeField] private GameObject deathUI;
    [SerializeField] private DeadPanelScript dps;
    [SerializeField] private DeadPanelScript deadctn;
    [SerializeField] private DeadPanelScript deadext;

    [SerializeField] private AudioSource cameraAudio;
    private PlayerController pc;
    private PlayerAttack pa;
    [SerializeField] private PortalScript ps;
    [SerializeField] private EnemyController ec;
    private PlayerHealth ph;
    [SerializeField] private EnemyRangeController erc;


    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pa = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        ps = GameObject.FindGameObjectWithTag("Portal").GetComponent<PortalScript>();
        ec = GameObject.FindGameObjectWithTag("MyEnemy").GetComponent<EnemyController>();
        erc = GameObject.FindGameObjectWithTag("MyEnemy").GetComponent<EnemyRangeController>();
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

        if (ps.enterPortal == true)
        {
            victoryUI.SetActive(true);
            vicText.SetActive(true);
            vstScript.ScaleVictoryText();
            ctnScript.ScaleVictoryText();
            extScript.ScaleVictoryText();
            ec.enabled = false;
            ph.enabled = false;
            erc.enabled = false;
        }
        else
            victoryUI.SetActive(false);

        if(pc.playerDead == true)
        {
            deathUI.SetActive(true);
            dps.ScaleDeadText();
            deadctn.ScaleDeadText();
            deadext.ScaleDeadText();
        }

    }

    void Resart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
        //Time.timeScale = 1f;
        pc.enabled = true;
        pa.enabled = true;
        ec.enabled = true;
        erc.enabled = true;
        cameraAudio.volume = 0.3f;
        Debug.Log("Restart");

    }

    void LoadMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("To Menu");
        pc.enabled = true;
        pa.enabled = true;
        SceneManager.LoadScene("MainMenu");
    }

    void Resume()
    {
        //disable canvas
        pauseUI.SetActive(false);

        //re-enable movement
        PausedGame = false;
        pc.enabled = true;
        pa.enabled = true;
        ec.enabled = true;
        erc.enabled = true;
        cameraAudio.volume = 0.3f;
    }

    void Pause()
    {
        //enabled canvas
        pauseUI.SetActive(true);

        //disable movement
        PausedGame = true;
        pc.enabled = false;
        pa.enabled = false;
        ec.enabled = false;
        erc.enabled = false;
        cameraAudio.volume = 0.15f;
    }

    void Continue()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Continued");
    }


}
