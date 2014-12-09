using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
public class BuildWithConfig : MonoBehaviour 
{
    [MenuItem("Eddy Tools/Windows Build With Config")]
    public static void BuildGame()
    {
        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose Build Location", "", "");
        List<string> buildAbleLevels = new List<string>();
        //PlayerSettings.levels
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            if (EditorBuildSettings.scenes[i].enabled)
                buildAbleLevels.Add(EditorBuildSettings.scenes[i].path);
        }
        string[] levels = new string[buildAbleLevels.Count];
        for (int i = 0; i < buildAbleLevels.Count; i++ )
        {
            levels[i] = buildAbleLevels[i];
        }
        BuildPipeline.BuildPlayer(levels, path + "/Game.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
        // Copy a file from the project folder to the build folder, alongside the built game.
        FileUtil.CopyFileOrDirectory("Config", path + "/Config");
    }
}
