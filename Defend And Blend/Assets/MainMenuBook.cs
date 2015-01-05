using UnityEngine;
using System.Collections;

public class MainMenuBook : MonoBehaviour 
{
    public Animator startBookAnimator;
    public Animator cameraAnimator;
	
    // Use this for initialization
    void Start()
    {
        startBookAnimator.SetBool("hasPressedPlay", true);
        cameraAnimator.SetTrigger("zoomOutToMainMenu");
    }
    
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
