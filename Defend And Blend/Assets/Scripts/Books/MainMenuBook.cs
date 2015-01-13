using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuBook : UI_GeneralBook 
{
    public Animator startBookAnimator;
    public Animator cameraAnimator;

    public Animator leftPageFadeOut;

    public Animator inputsAnimator;
    public Text UserIDtxt;
    public Text SessionIDtxt;

    public Toggle onlyGrabToggle;
    public Toggle onlyXToggle;
    public Toggle onlyYToggle;
    public Toggle autoToggle;
    public Toggle manualToggle;

   // public UI_GeneralBook bookSettings;
    // Use this for initialization
    void Awake()
    {
        turnOffAllRightPages();
    }
    protected override void Start()
    {
        startBookAnimator.SetBool("hasPressedPlay", true);
        cameraAnimator.SetTrigger("zoomOutToMainMenu");

        StartCoroutine(WaitTillBookIsHalfOpen(55 * Time.deltaTime));
        leftPageFadeOut.SetTrigger("isFadingIn");

        inputsAnimator.SetTrigger("FadeIn");

        onlyGrabToggle.isOn = false;
        onlyXToggle.isOn = false;
        onlyYToggle.isOn = false;
        autoToggle.isOn = false;
        manualToggle.isOn = true;
        base.Start();
        
    }
    
	
	// Update is called once per frame
	void Update () 
    {
        UserIDtxt.GetComponent<Text>().text = "Player ID: " + GameValues.USERID;
        SessionIDtxt.GetComponent<Text>().text = "Session ID: " + GameValues.SESSIONID; 
	}

    public void AutoPlay()
    {
        GameValues.AutoGrab = true;
        GameValues.AutoMoveX = true;
        GameValues.AutoMoveY = true;

        onlyGrabToggle.isOn = false;
        onlyXToggle.isOn = false;
        onlyYToggle.isOn = false;
        manualToggle.isOn = false;

    }
    public void AutoMoveXY()
    {
        GameValues.AutoGrab = false;
        GameValues.AutoMoveX = true;
        GameValues.AutoMoveY = true;

        onlyXToggle.isOn = false;
        onlyYToggle.isOn = false;
        autoToggle.isOn = false;
        manualToggle.isOn = false;

    }
    public void AutoGrabMoveX()
    {
        GameValues.AutoGrab = true;
        GameValues.AutoMoveX = true;
        GameValues.AutoMoveY = false;

        onlyGrabToggle.isOn = false;
        onlyXToggle.isOn = false;
        autoToggle.isOn = false;
        manualToggle.isOn = false;

    }
    public void AutoGrabMoveY()
    {
        GameValues.AutoGrab = true;
        GameValues.AutoMoveX = false;
        GameValues.AutoMoveY = true;

        onlyGrabToggle.isOn = false;
        onlyYToggle.isOn = false;
        autoToggle.isOn = false;
        manualToggle.isOn = false;

    }
    public void PlayManually()
    {
        GameValues.AutoGrab = false;
        GameValues.AutoMoveX = false;
        GameValues.AutoMoveY = false;

        onlyGrabToggle.isOn = false;
        onlyXToggle.isOn = false;
        onlyYToggle.isOn = false;
        autoToggle.isOn = false;
    }
    public void StartNewGame()
    {
        Debug.Log("A new game has started.");
        //BookAnimator.SetTrigger("turnPage_anim");
        Application.LoadLevel("level_1");
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

    public void backToStartBook()
    {
        inputsAnimator.SetTrigger("FadeOut");
        leftPageFadeOut.SetTrigger("isGoingBack");
        cameraAnimator.SetTrigger("testje");
        startBookAnimator.SetBool("hasPressedPlay", false);

        print("Starting " + Time.time);
        StartCoroutine(WaitAndPrint(55 * Time.deltaTime));
        print("Before WaitAndPrint Finishes " + Time.time);
        
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel("Level_00_StartBook");
        Debug.Log("I'm done with this...");
    }
    IEnumerator WaitTillBookIsHalfOpen(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
    }
}
