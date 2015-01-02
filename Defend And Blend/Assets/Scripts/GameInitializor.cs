using UnityEngine;
using System.Collections;
//using Parse;
using System.Threading.Tasks;


public class GameInitializor : MonoBehaviour 
{
    public string userID;
    public string sessionID;
    
    // Use this for initialization
	void Start () 
    {
	    //Init game stuff here

        //UserNameGO.


        GameValues.Reset();
        //DataLoader dl = new DataLoader();
        /*
        ParseObject gameScore = new ParseObject("GameScore");
        gameScore["score"] = 1337;
        gameScore["playerName"] = "Sean Plott";
        Task saveTask = gameScore.SaveAsync();
         */
	}

    public void setUserName()
    {
        //userID = ;
    }
}
