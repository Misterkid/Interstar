using UnityEngine;
using System.Collections;

public class CatchTrigger : MonoBehaviour 
{
    public bool destroyWholeObject = true;
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
            if (destroyWholeObject)
                Destroy(pickUp.gameObject);
            else
                Destroy(pickUp);

            GameValues.HANGAR.Score++;
        }
    }
}
