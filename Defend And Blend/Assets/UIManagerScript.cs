using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour 
{
  
    public Animator CameraAnimator;
    public Animator BookAnimator;

    public GameObject rp_StartGame;
    public GameObject rp_Options;
    public GameObject rp_Highscores;
    public GameObject rp_Credits;

    public AudioSource backgroundMusic;
    
    void Awake()
    {
        turnOffAllRightPages();
    }

	// Use this for initialization
	void Start () 
    {
       CameraAnimator.SetBool("GameIsPaused", false);     
	}

    public void StartNewGame()
    {
        Application.LoadLevel("healthbar_test");
    }

	public void openStartGame()
    {
        //First we need to turn off the other pages.
        turnOffAllRightPages();

        if (rp_StartGame.active == true)
        {
            rp_StartGame.SetActive(false);
        }
        else if (rp_StartGame.active == false)
        {
            rp_StartGame.SetActive(true);
        }
    }
    public void openOptions()
    {
        //First we need to turn off the other pages.
        turnOffAllRightPages();

        if (rp_Options.active == true)
        {
            rp_Options.SetActive(false);
        }
        else if (rp_Options.active == false)
        {
            rp_Options.SetActive(true);
        }
    }

    public void openHighscores()
    {
        //First we need to turn off the other pages.
        turnOffAllRightPages();

        if (rp_Highscores.active == true)
        {
            rp_Highscores.SetActive(false);
        }
        else if (rp_Highscores.active == false)
        {
            rp_Highscores.SetActive(true);
        }
    }

    public void openCredits()
    {
        //First we need to turn off the other pages.
        turnOffAllRightPages();

        if (rp_Credits.active == true)
        {
            rp_Credits.SetActive(false);
        }
        else if (rp_Credits.active == false)
        {
            rp_Credits.SetActive(true);
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void turnOffAllRightPages()
    {
        rp_StartGame.SetActive(false);
        rp_Options.SetActive(false);
        rp_Highscores.SetActive(false);
        rp_Credits.SetActive(false);
    }

    void Update()
    {
        //BookAnimator.Play(stateName: turnPage_anim, );
    }

    public void setGameVolume(float vol)
    {
        backgroundMusic.volume = vol;
    }
}
