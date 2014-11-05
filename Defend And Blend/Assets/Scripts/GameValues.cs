using UnityEngine;
using System.Collections;

public class GameValues 
{
    public static int SCORE = 0;
    public static int CURRENTWAVE = 0;
    public static void Reset()
    {
        SCORE = 0;
        CURRENTWAVE = 0;
    }
}
