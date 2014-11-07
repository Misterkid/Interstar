using UnityEngine;
using System.Collections;

public class GameInitializor : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	    //Init game stuff here
        GameValues.Reset();
        SoundManager.Load();//Initialize SoundManager.
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
