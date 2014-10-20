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
        //isFinished = Animator.StringToHash("Base Layer.StartGame");
    }
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log(StartGame.GetBool("hasClickedStart"));
        //Debug.Log(StartGame.GetBool("isDone"));

        if (StartGame.GetBool("hasClickedStart"))
        {
            // WaitForSeconds(5);
            Debug.Log(Camera.main.transform.position);
            if (Camera.main.transform.position.x == 0)
            {
                StartGame.SetBool("isDone", true);
            }
            if (StartGame.GetBool("isDone") == true)
            {
                //LoadLevel(2);
            }
        }
        
        // Debug.Log(isFinished);
        
	}
    
    public void LoadLevel(int id)
    {
        if (StartGame.GetBool("isDone") == true)
        {
            Application.LoadLevel(id);
            Debug.Log("?");
        }

    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void startTheGame()
    {
        StartGame.SetBool("hasClickedStart", true);
        //yield WaitForSeconds (5);
        //WaitForSeconds(5);
       
    }

   
    
}
