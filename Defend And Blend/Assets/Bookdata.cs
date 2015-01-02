using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class Bookdata : MonoBehaviour {

    public GameValues theGameValues;

    public GameObject UserIDGameObject;
    public string Teststring;
    
    void Awake()
    {
        Debug.Log("Startbook has been loaded");
    }
    // Use this for initialization
	void Start() 
    {
       
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        Teststring = UserIDGameObject.GetComponent<InputField>().text;
        Debug.Log(Teststring);
	}

    public void LoadFirstLevel()
    {
        if (Teststring.Length > 0)
        {
            
            //theGameValues.
        }
        //string _password = inputPassword.GetComponent<InputField>().text;
        //UserIDGameObject.GetComponent<InputField>().text;

        

        Application.LoadLevel("Level_1");
    }
}
