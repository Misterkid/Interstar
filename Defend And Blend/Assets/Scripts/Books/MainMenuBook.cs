using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuBook : UI_GeneralBook 
{
    public Animator startBookAnimator;
    public Animator cameraAnimator;
    

    public Text UserIDtxt;
    public Text SessionIDtxt;
     
   

   // public UI_GeneralBook bookSettings;
    // Use this for initialization
    void Awake()
    {
        turnOffAllRightPages();
    }
    
    void Start()
    {
        startBookAnimator.SetBool("hasPressedPlay", true);
        cameraAnimator.SetTrigger("zoomOutToMainMenu");

        Debug.Log(GameValues.USERID);
    }
    
	
	// Update is called once per frame
	void Update () 
    {
        UserIDtxt.GetComponent<Text>().text = "Player ID: " + GameValues.USERID;
        SessionIDtxt.GetComponent<Text>().text = "Session ID: " + GameValues.SESSIONID; 
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
        Debug.Log("backToStart.... 1");
        cameraAnimator.SetTrigger("testje");
        Debug.Log(cameraAnimator);
        Debug.Log("backToStart.... 2");

        //Application.LoadLevel("Level_00_StartBook");
    }
    
}
