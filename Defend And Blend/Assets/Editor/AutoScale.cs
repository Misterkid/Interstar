using UnityEngine;
using System.Collections;
using UnityEditor;
public class AutoScale : AssetPostprocessor 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    void OnPreprocessModel()
    {
        ModelImporter modelImporter = assetImporter as ModelImporter;
        modelImporter.globalScale = 1;
        Debug.Log("Object Scaled to 1");
    }

}
