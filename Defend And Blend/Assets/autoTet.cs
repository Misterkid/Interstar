using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class autoTet : MonoBehaviour 
{
    string welcomeText = "Welcome to the tutorial of 'Defend and Blend'";
    string secondText = "Your objective is to defend the blender from the friuits that are trying to destroy it";
    Text welcome; 

	// Use this for initialization
	void Start () 
    {
        welcome = gameObject.GetComponent<Text>();
        StartCoroutine("AutoType");
	}
	
	// Update is called once per frame
    IEnumerator AutoType()
    {
        foreach (char letter in welcomeText.ToCharArray())
        {
            welcome.text += letter;
            yield return new WaitForSeconds(0.5f);
        }
        
        
    }
}
