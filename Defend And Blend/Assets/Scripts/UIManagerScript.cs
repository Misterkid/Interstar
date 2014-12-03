using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Parse;

public class UIManagerScript : MonoBehaviour 
{
    public Animator CameraAnimator;
    public Animator BookAnimator;

    public GameObject rp_StartGame;
    public GameObject rp_Options;
    public GameObject rp_Highscores;
    public GameObject rp_Credits;

    public Slider backgroundmusic;
    public Slider soundeffects;

    bool gameIsPaused = false;



    public Text TestText;
  
//private Task signUpTask = user.SignUpAsync();

    //public AudioSource backgroundMusic;
    
    void Awake()
    {
        turnOffAllRightPages();
    }

	// Use this for initialization
	void Start () 
    {
        backgroundmusic.value = SoundManager.Instance.soundValues[SoundManager.SoundTypes.MUSIC];
        soundeffects.value = SoundManager.Instance.soundValues[SoundManager.SoundTypes.EFFECT];

        if (CameraAnimator != null)
            CameraAnimator.SetTrigger("ToGame");
            //CameraAnimator.SetBool("GameIsPaused", true);
        //StartCoroutine("waitTillPauseAnimation");
        //Time.timeScale = 0;

        
	}

    void Update()
    {

        //Debug.Log(gameIsPaused);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameValues.ISPAUSED = GameValues.ISPAUSED ? false : true;

            if (GameValues.ISPAUSED == true)
            {
                if (CameraAnimator != null)
                    CameraAnimator.SetTrigger("ToPause");
                    //CameraAnimator.SetBool("GameIsPaused", false);
            }
            else
            {
                if (CameraAnimator != null)
                    CameraAnimator.SetTrigger("ToGame");
                    //CameraAnimator.SetBool("GameIsPaused", true);
            }
            Debug.Log("Is het spel op pauze? " + GameValues.ISPAUSED);
        }
    }

    public void StartNewGame()
    {
        BookAnimator.SetTrigger("turnPage_anim");
        Application.LoadLevel("level_1");
    }

	public void openStartGame()
    {
        //First we need to turn off the other pages.
        turnOffAllRightPages();
        BookAnimator.SetTrigger("turnPage_anim");
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
        BookAnimator.SetTrigger("turnPage_anim");
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
        BookAnimator.SetTrigger("turnPage_anim");
        turnOffAllRightPages();
        
        if (rp_Highscores.active == true)
        {
            rp_Highscores.SetActive(false);
        }
        else if (rp_Highscores.active == false)
        {
            rp_Highscores.SetActive(true);
            //TestText
        }
    }

    public void openCredits()
    {
        //First we need to turn off the other pages.
        turnOffAllRightPages();
        
        BookAnimator.SetTrigger("turnPage_anim");
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
    public void setMusicVolume(float vol)
    {
        //backgroundMusic.volume = vol;
        //SoundManager.Instance.PlaySound()
        SoundManager.Instance.ChangeVolume(vol, SoundManager.SoundTypes.MUSIC);

    }

    public void setSoundEffectsVolume(float vol)
    {
        SoundManager.Instance.ChangeVolume(vol, SoundManager.SoundTypes.EFFECT);
    }
}
