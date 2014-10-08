using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour 
{
    public Animator StartGame;
        
    // Use this for initialization
	void Start () 
    {

    }
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
    public void LoadLevel(int id)
    {
        Application.LoadLevel(id);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void startTheGame()
    {
        StartGame.SetBool("hasClickedStart", true);
       
       
    }

   
    
}
