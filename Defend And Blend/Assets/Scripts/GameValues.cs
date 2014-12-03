using UnityEngine;
using System.Collections;
using System.IO;
using Parse;
using System.Threading.Tasks;


public class GameValues 
{

    //public user = ParseUser.CurrentUser;
    public static int SCORE = 0;
    public static int CURRENTWAVE = 0;
    public static bool ISPAUSED = false;
    public static void Reset()
    {
        SCORE = 0;
        CURRENTWAVE = 0;
        ISPAUSED = false;
    }
    public void Start()
    {
        ParseObject gameScore = new ParseObject("GameScore");
        gameScore["score"] = 100;
        gameScore["playerID"] = ParseUser.CurrentUser;
        Task saveTask = gameScore.SaveAsync();
        
    }
}
