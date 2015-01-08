using UnityEngine;
using System.Collections;

public class inGamePause : MonoBehaviour {


    public Animator startBookAnimator;
    public Animator cameraAnimator;

    void Awake()
    {
        startBookAnimator.SetBool("isAlreadyOpen", true);
        //cameraAnimator.SetTrigger("zoomOutToMainMenu");
    }
    
    void quitToMainMenu()
    {

    }
}
