using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour 
{
    public Animator RP_StartGame;
    public Animator BookAnimator;

	// Use this for initialization
	void Awake() 
	{
        Debug.Log("Awake Function has been called.");
        RP_StartGame.SetBool("isVisible", false);
        Debug.Log(RP_StartGame.GetBool("isVisible")); 

        //
	}
	
	// Update is called once per frame
	void Update () 
	{
        
	}

	// Update is called once per frame
	public void openStartGameOptions() 
	{
        //Debug.Log("Start Game has been clicked");
        
        if (RP_StartGame.GetBool("isVisible") == true)
        {
            RP_StartGame.SetBool("isVisible", false);
        }
        else //RP_StartGame.SetBool("isVisible", false);
        {
            RP_StartGame.SetBool("isVisible", true);
        }

	}
}
