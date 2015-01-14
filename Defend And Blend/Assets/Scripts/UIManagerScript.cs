using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class UIManagerScript : MonoBehaviour 
{
    public Animator CameraAnimator;
    public Animator BookAnimator;

    public Text gameScoreText;

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
    public Animator hudBoven;

       
    public HelpingHand theHand;

    
    void Awake()
    {
        turnOffButtons();
        turnOffAllRightPages();
    }

	// Use this for initialization
	void Start () 
    {

        
        if (backgroundmusic != null)
        backgroundmusic.value = SoundManager.Instance.soundValues[SoundManager.SoundTypes.MUSIC];
        if (backgroundmusic != null)
        soundeffects.value = SoundManager.Instance.soundValues[SoundManager.SoundTypes.EFFECT];



        if (backgroundmusic != null)
            backgroundmusic.value = SoundManager.Instance.soundValues[SoundManager.SoundTypes.MUSIC];
        if (soundeffects != null)
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
                hudBoven.SetTrigger("toPause");
                if (CameraAnimator != null)
                    CameraAnimator.SetTrigger("ToPause");
                    //CameraAnimator.SetBool("GameIsPaused", false);
            }
            else
            {
                hudBeneden.SetTrigger("ToGame");
                hudBoven.SetTrigger("toGame");
                turnOffButtons();
                turnOffAllRightPages();
                if (CameraAnimator != null)
                    CameraAnimator.SetTrigger("ToGame");
                    //CameraAnimator.SetBool("GameIsPaused", true);
                
            }
            if (gameScoreText != null)
            {
                gameScoreText.text = "Score: " + GameValues.SCORE;
            }
            Debug.Log("Is het spel op pauze? " + GameValues.ISPAUSED);
        }
    }

    
    public void continueGame()
    {
        hudBeneden.SetTrigger("ToGame");
        hudBoven.SetTrigger("toGame");
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
    



    
}
