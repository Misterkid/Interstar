using UnityEngine;
using System.Collections;
using System.IO;




public class GameValues 
{
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
       /* ParseObject testObject = new ParseObject("TestObject");
        testObject["foo"] = "bar";
        testObject.SaveAsync();

        */
        
    }
}
