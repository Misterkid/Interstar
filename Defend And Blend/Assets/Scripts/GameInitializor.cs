using UnityEngine;
using System.Collections;
using Parse; 

public class GameInitializor : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
	    //Init game stuff here
        GameValues.Reset();


       /*

        ParseObject gameScore = new ParseObject("GameScore");
        gameScore["score"] = 1337;
        gameScore["playerName"] = "Sean Plott";
        gameScore.SaveAsync();
      */
	}
}
