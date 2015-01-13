using UnityEngine;
using System.Collections;

public class inGamePause : UI_GeneralBook {


    public Animator startBookAnimator;
    public Animator cameraAnimator;

    void Awake()
    {
        startBookAnimator.SetBool("isAlreadyOpen", true);
        //cameraAnimator.SetTrigger("zoomOutToMainMenu");
    }
    
    public void quitToMainMenu()
    {
        Application.LoadLevel(1);
    }
}
