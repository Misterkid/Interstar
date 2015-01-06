using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuBook : MonoBehaviour 
{
    public Animator startBookAnimator;
    public Animator cameraAnimator;

    public Text UserIDtxt;
    public Text SessionIDtxt;

    // Use this for initialization
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
}
