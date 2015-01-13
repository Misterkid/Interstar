using UnityEngine;
using System.Collections;
using System.IO;



public class GameValues 
{
 
    public static int SCORE = 0;
    public static int CURRENTWAVE = 0;
    public static bool ISPAUSED = false;
    public static int SMOOTHYPOINTS = 0;
    //This depends on everytrainee.
    public static string USERID;
    public static string SESSIONID;

    public static bool AutoMoveX = false;
    public static bool AutoMoveY = false;
    public static bool AutoGrab = false;


    public static int BlenderFilledPoints = 0;

    public static void Reset()
    {
        SCORE = 0;
        CURRENTWAVE = 0;
        SMOOTHYPOINTS = 0;
        ISPAUSED = false;
        BlenderFilledPoints = 0;
    }
    public void Start()
    {
        
        
    }
}
