using UnityEngine;
using System.Collections;
using System.IO;
using Parse;
using System.Threading.Tasks;


public class GameValues 
{
 
    public static int SCORE = 0;
    public static int CURRENTWAVE = 0;
    public static bool ISPAUSED = false;
    public static int SMOOTHYPOINTS = 0;
    //This depends on everytrainee.
    public static string USERID;
    public static string SESSIONID;

    public static void Reset()
    {
        SCORE = 0;
        CURRENTWAVE = 0;
        SMOOTHYPOINTS = 0;
        ISPAUSED = false;
    }
    public void Start()
    {
        
        
    }
}
