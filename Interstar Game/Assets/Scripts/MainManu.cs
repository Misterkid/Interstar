using UnityEngine;
using System.Collections;

public class MainManu : MonoBehaviour 
{
    private Animator animator;
	// Use this for initialization
	void Start () 
    {
        animator = gameObject.GetComponent<Animator>();//Get the Animator
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
    //Start the game by calling the StartAnimation Trigger
    public void StartTheGame()
    {
        animator.SetTrigger("StartAnimation");
    }
    //In The animation(At the end) this Event function will be called! 
    //How? http://docs.unity3d.com/Manual/animeditor-AnimationEvents.html
    public void OnCameraAnimationExit(int id)
    {
        Application.LoadLevel(id);//Load new level
    }
    public void LoadLevel(int id)
    {
        Application.LoadLevel(id);
    }
    public void Quit()
    {
        Application.Quit();//Quit!
    }
}
