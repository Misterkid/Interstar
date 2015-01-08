using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuBook : UI_GeneralBook 
{
    public Animator startBookAnimator;
    public Animator cameraAnimator;

    public Animator leftPageFadeOut;

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

        StartCoroutine(WaitTillBookIsHalfOpen(55 * Time.deltaTime));
        leftPageFadeOut.SetTrigger("isFadingIn");


        
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
