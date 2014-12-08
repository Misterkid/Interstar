using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;


public class GameInitializor : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
	    //Init game stuff here
        GameValues.Reset();
        //DataLoader dl = new DataLoader();
        /*
        ParseObject gameScore = new ParseObject("GameScore");
        gameScore["score"] = 1337;
        gameScore["playerName"] = "Sean Plott";
        Task saveTask = gameScore.SaveAsync();
         */
	}
}
