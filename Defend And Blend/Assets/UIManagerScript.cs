using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour 
{
  
    public Animator CameraAnimator;
    public Animator BookAnimator;
    public Animator rp_StartGame;
    public GameObject wooot;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () 
    {
       CameraAnimator.SetBool("GameIsPaused", false);

       //rp_StartGame.SetBool("isInVisible", true);
       //Debug.Log(rp_StartGame.GetBool("isInVisible"));

       //
	}

    public void Update()
    {
       
    }
	
	public void StartGame()
    {
        if (rp_StartGame.GetBool("isInVisible") == true)
        {
            rp_StartGame.SetBool("isInVisible", false);
        }
        else if (rp_StartGame.GetBool("isInVisible") == false)
        {
            rp_StartGame.SetBool("isInVisible", true);
        }
    }

   
}
