using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bookdata : MonoBehaviour {

    public GameObject UserIDGameObject;
    public GameObject SessionIDGameObject;

    public GameObject bookCoverInfo;

    // Load the two buttons (Play and Quit)
    public Button toMainMenu;
    public Button toWindows;
    public Animator cameraAnimator;
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
        
	}

    public void LoadFirstLevel()
    {

        cameraAnimator.SetTrigger("zoomOutToMainMenu");
        StartCoroutine(WaitForLoad(45 * Time.deltaTime));

        if (UserIDGameObject.GetComponent<InputField>().text.Length > 0 && SessionIDGameObject.GetComponent<InputField>().text.Length > 0)
        {

            GameValues.USERID = UserIDGameObject.GetComponent<InputField>().text;
            GameValues.SESSIONID = SessionIDGameObject.GetComponent<InputField>().text;
        }
    }
    private IEnumerator WaitForLoad(float time)
    {
        yield return new WaitForSeconds(time);
        bookCoverInfo.SetActive(false);;
        Application.LoadLevel("Level_00_MainMenu");
    }
    public void quitToDesktop()
    {
        Application.Quit();
    }
}
