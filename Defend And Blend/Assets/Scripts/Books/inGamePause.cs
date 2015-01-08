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
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
