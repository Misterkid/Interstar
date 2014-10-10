using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour 
{
    public Animator StartGame;
    public int isFinished;
    

    // Use this for initialization
	void Start () 
    {
        isFinished = Animator.StringToHash("Base Layer.StartGame");
    }
	
	// Update is called once per frame
	void Update () 
    {
        Debug.Log(isFinished);
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
        Debug.Log(StartGame);

        

        
        //StartGame.Stop();
        //Debug.Log(StartGame.GetCurrentAnimatorStateInfo(0));

        //StartGame.
       
    }

   
    
}
