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

    public Button continueButton;
    public Button newGameButton;
    public Button optionsButton;
    public Button creditsButton;
    public Button mainMenuButton;

    // Trying to fade in and out the HUD while on pause.
    public Animator hudBeneden;


       
    public HelpingHand theHand;


  
//private Task signUpTask = user.SignUpAsync();

    //public AudioSource backgroundMusic;
    
    void Awake()
    {
        turnOffButtons();
        turnOffAllRightPages();
    }

	// Use this for initialization
	void Start () 
    {
        

        backgroundmusic.value = SoundManager.Instance.soundValues[SoundManager.SoundTypes.MUSIC];
        soundeffects.value = SoundManager.Instance.soundValues[SoundManager.SoundTypes.EFFECT];
       
        //helpingHand = GameObject.FindObjectOfType<HelpingHand>();

        if (CameraAnimator != null)
            CameraAnimator.SetTrigger("ToGame");
            //CameraAnimator.SetBool("GameIsPaused", true);
       
        
        
	}

    void turnOffButtons()
    {
        if (continueButton != null)
        {
            continueButton.interactable = false;
        }
        newGameButton.interactable = false;
        creditsButton.interactable = false;
        optionsButton.interactable = false;
        mainMenuButton.interactable = false;
    }

    void turnOnButtons()
    {
        if (continueButton != null)
        {
            continueButton.interactable = true;
        }
        newGameButton.interactable = true;
        creditsButton.interactable = true;
        optionsButton.interactable = true;
        mainMenuButton.interactable = true;
    }

    void Update()
    {
       // Debug.Log(optionsButton.IsInteractable());
        //Debug.Log(GameValues.ISPAUSED);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameValues.ISPAUSED = GameValues.ISPAUSED ? false : true;

            if (GameValues.ISPAUSED == true)
            {
                turnOnButtons();
                hudBeneden.SetTrigger("ToPause");

                if (CameraAnimator != null)
                    CameraAnimator.SetTrigger("ToPause");
                    //CameraAnimator.SetBool("GameIsPaused", false);
            }
            else
            {
                hudBeneden.SetTrigger("ToGame");
                turnOffButtons();
                turnOffAllRightPages();
                if (CameraAnimator != null)
                    CameraAnimator.SetTrigger("ToGame");
                    //CameraAnimator.SetBool("GameIsPaused", true);
                
            }
            Debug.Log("Is het spel op pauze? " + GameValues.ISPAUSED);
        }
    }

    public void StartNewGame()
    {
        Debug.Log("A new game has started.");
        //BookAnimator.SetTrigger("turnPage_anim");
        Application.LoadLevel("level_1");
    }
    public void continueGame()
    {
        hudBeneden.SetTrigger("ToGame");
        Debug.Log("You pressed continue.");
        turnOffButtons();
        turnOffAllRightPages();
        GameValues.ISPAUSED = false;
        if (CameraAnimator != null)
            CameraAnimator.SetTrigger("ToGame");
    }

	public void openStartGame()
    {
        //First we need to turn off the other pages.
        turnOffAllRightPages();
        //BookAnimator.SetTrigger("turnPage_anim");
        if (rp_StartGame != null)
        {

	        if (rp_StartGame.active == true)
	        {
	            rp_StartGame.SetActive(false);
	        }
	        else if (rp_StartGame.active == false)
	        {
	            rp_StartGame.SetActive(true);
	        }
        }
    }
    public void openOptions()
    {
        //First we need to turn off the other pages.
        turnOffAllRightPages();
        //BookAnimator.SetTrigger("turnPage_anim");
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
        //BookAnimator.SetTrigger("turnPage_anim");
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
        
        //BookAnimator.SetTrigger("turnPage_anim");
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
        if (rp_StartGame != null)
        {
        	rp_StartGame.SetActive(false);
        }
        rp_Options.SetActive(false);
        if (rp_StartGame != null)
        {
            rp_Highscores.SetActive(false);
        }
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

    public void setAutoXMovement(bool autoX)
    {
        theHand.AutoMoveX = autoX;
        
        Debug.Log("X-as goes automaticly? :" + theHand.AutoMoveX);
    }
    public void setAutoYMovement(bool autoY)
    {
        theHand.AutoMoveY = autoY;
        Debug.Log("Y-as goes automaticly? :" + theHand.AutoMoveY);
    }
    public void setAutoGrab(bool autoGrab)
    {
        theHand.AutoGrab = autoGrab;
        Debug.Log("Grabbing Automaticly? :" + theHand.AutoGrab);
    }



    
}
