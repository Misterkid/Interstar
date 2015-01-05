using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bookdata : MonoBehaviour {

    public GameValues theGameValues;

    public GameObject UserIDGameObject;
    public string Teststring;

    // This Animator is made to open the book.
    public Animator startBookAnimator;
    public GameObject bookCoverInfo;

    // Load the two buttons (Play and Quit)
    public Button toMainMenu;
    public Button toWindows;
    
    void Awake()
    {
        //Debug.Log("Startbook has been loaded");

        //Turn off the open-animation



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
        bookCoverInfo.SetActive(false);
        startBookAnimator.SetBool("hasPressedPlay", true);
        
        if (Teststring.Length > 0)
        {
            
            //theGameValues.
        }
        //string _password = inputPassword.GetComponent<InputField>().text;
        //UserIDGameObject.GetComponent<InputField>().text;

        

        //Application.LoadLevel("Level_1");
    }
    
    public void quitToDesktop()
    {
        Application.Quit();
    }
}
