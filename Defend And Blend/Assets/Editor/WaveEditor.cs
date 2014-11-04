using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
[CustomEditor(typeof(WaveSpawnerTwo))]
public class WaveEditor : Editor
{
    private string monstersText = "";
    private string[] monsters;
    private string wave;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        WaveSpawnerTwo waveSpawner = target as WaveSpawnerTwo;
        //List<Wave> waves = helpingHand.waves;
        Wave[] waves = waveSpawner.waves;

        EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("WaveNo:");
                wave = EditorGUILayout.TextField(wave);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Monsters(0,0,etc):");
                monstersText = EditorGUILayout.TextField(monstersText);
            EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("AddWave"))
        {
            monsters = monstersText.Split(',');
            int waveNo = int.Parse(wave);

            if(waves.Length == waveNo)//Lets add a wave
            {
                Wave[] clone = waves;
                waves = new Wave[clone.Length + 1];
                for(int i = 0; i < clone.Length; i++)
                {
                    waves[i] = clone[i];
                }
                waves[waves.Length - 1] = new Wave();
                Debug.Log("ADDING A WAVE");
            }
            else if (waves.Length  < waveNo)
            {
                Debug.LogError("ERROR CANNOT ADD");
                return;
            }
            else
            {
                if (waves.Length == 0)
                {
                    waves = new Wave[1];
                    waves[0] = new Wave();
                }
                Debug.Log("EDITING WAVE");
            }
            waves[waveNo].monsters = new int[monsters.Length];
            for(int i = 0; i < monsters.Length; i ++)
            {
                int monsterNo = int.Parse(monsters[i]);
                waves[waveNo].monsters[i] = monsterNo;
            }
            Debug.Log("DONE!");
        }
        EditorGUILayout.EndVertical();
        waveSpawner.waves = waves;
        serializedObject.ApplyModifiedProperties();
    }
}
