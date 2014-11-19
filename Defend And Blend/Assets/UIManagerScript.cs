using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour 
{
    

    public Animator CameraAnimator;

  //  public Animator OptionsTextAnimator;
  //  public Animator HighscoresAnimator;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () 
    {
       CameraAnimator.SetBool("GameIsPaused", false);
        //OptionsTextAnimator.SetBool("isHidden", true);
        //HighscoresAnimator.SetBool("isHidden", true);
	}

    public void Update()
    {
       // Debug.Log(CameraAnimator.GetBool("GameIsPaused"));
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Debug.Log("Escape	 is ingedrukt!");
            if (!CameraAnimator.GetBool("GameIsPaused"))
            {
                //gameIsPaused = true;
                CameraAnimator.SetBool("GameIsPaused", true);
                Debug.Log("Leuk Spelletje is op pauze gezet");
            }
            else if (CameraAnimator.GetBool("GameIsPaused"))
            {
                CameraAnimator.SetBool("GameIsPaused", false);
                Debug.Log("Leuk Spelletje is nu niet meer op pauze hoor!");

                // Misschien komt hier een countdown (3, 2, 1)
                // Wanneer het spelletje start na de animatie.
            }   
                
        }
    }
	
	public void StartGame()
    {
        Application.LoadLevel("Eddy_test");
    }

    public void openOptions()
    {
        //Wanneer isHidden al true is, maak hem false
        /*
        if (OptionsTextAnimator.GetBool("isHidden"))
        {
            HighscoresAnimator.SetBool("isHidden", true);
            OptionsTextAnimator.SetBool("isHidden", false);
        }
        else if (!OptionsTextAnimator.GetBool("isHidden"))
        {
            OptionsTextAnimator.SetBool("isHidden", true);
        }
        */
    }

    public void openHighScores()
    {
        /*
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
        */
    }
    public void openPauseScreen()
    {
       
    }
}
