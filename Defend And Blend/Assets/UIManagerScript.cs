using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour 
{
    public Animator OptionsTextAnimator;

	// Use this for initialization
	void Start () 
    {
        OptionsTextAnimator.SetBool("isHidden", true);
	}
	
	public void StartGame()
    {
        Application.LoadLevel("Eddy_test");
    }

    public void openOptions()
    {
        //Wanneer isHidden al true is, maak hem false
        if (OptionsTextAnimator.GetBool("isHidden"))
        {
            OptionsTextAnimator.SetBool("isHidden", false);
        }
        else if (!OptionsTextAnimator.GetBool("isHidden"))
        {
            OptionsTextAnimator.SetBool("isHidden", true);
        }
    }
}
