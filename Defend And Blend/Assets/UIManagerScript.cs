using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour 
{
    public Animator OptionsTextAnimator;
    public Animator HighscoresAnimator;

	// Use this for initialization
	void Start () 
    {
        OptionsTextAnimator.SetBool("isHidden", true);
        HighscoresAnimator.SetBool("isHidden", true);
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
            HighscoresAnimator.SetBool("isHidden", true);
            OptionsTextAnimator.SetBool("isHidden", false);
        }
        else if (!OptionsTextAnimator.GetBool("isHidden"))
        {
            OptionsTextAnimator.SetBool("isHidden", true);
        }
    }

    public void openHighScores()
    {
        //Wanneer isHidden al true is, maak hem false
        if (HighscoresAnimator.GetBool("isHidden"))
        {
            HighscoresAnimator.SetBool("isHidden", false);
            OptionsTextAnimator.SetBool("isHidden", true);
        }
        else if (!HighscoresAnimator.GetBool("isHidden"))
        {
            HighscoresAnimator.SetBool("isHidden", true);
        }
    }
}
