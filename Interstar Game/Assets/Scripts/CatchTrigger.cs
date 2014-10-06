using UnityEngine;
using System.Collections;

public class CatchTrigger : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    void OnTriggerEnter(Collider collider)
    {
        PickUps pickUp = collider.gameObject.GetComponent<PickUps>();
        if (pickUp != null)
        {
            Destroy(pickUp.gameObject);
            GameValues.HANGAR.Score++;
        }
    }
}
