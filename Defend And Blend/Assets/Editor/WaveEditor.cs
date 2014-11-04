using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
[CustomEditor(typeof(Wave))]
public class WaveEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
       // HelpingHand helpingHand = target as HelpingHand;
        //List<Wave> waves = helpingHand.waves;
      //  Wave[] waves = helpingHand.waves;

        /*
        if (GUILayout.Button("AddWave"))
        {
           // TileGraphicMap tileMap = (TileGraphicMap)target as TileGraphicMap;
            //tileMap.BuildMesh();
        }
         */ 
    }
}
