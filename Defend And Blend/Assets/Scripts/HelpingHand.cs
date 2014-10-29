using UnityEngine;
using System.Collections;

public class HelpingHand : MonoBehaviour 
{
    public GameObject rightObject;
    public GameObject leftObject;
    private float rightObjectStart;
    private float leftObjectStart;
	// Use this for initialization
	void Start () 
    {
        rightObjectStart = rightObject.transform.localPosition.x;
        leftObjectStart = leftObject.transform.localPosition.x;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    //Controls
        transform.Translate((Input.GetAxis("Horizontal") * 10) * Time.deltaTime, (Input.GetAxis("Vertical") * 10) * Time.deltaTime, 0);
        //pressure
        if(rightObject != null && leftObject != null)
        {
            float squeezePressure = rightObjectStart - (rightObjectStart * Input.GetAxis("RTrigger"));//Get the trigger / pressure value.
            rightObject.transform.localPosition = new Vector3(squeezePressure, rightObject.transform.localPosition.y, rightObject.transform.localPosition.z);
            leftObject.transform.localPosition = new Vector3(-squeezePressure, leftObject.transform.localPosition.y, leftObject.transform.localPosition.z);
           // collider.bounds.size = new Vector3(collider.bounds.size.x, collider.bounds.size.y, collider.bounds.size.z);
            BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>() as BoxCollider;
            boxCollider.size = new Vector3( (rightObject.transform.localPosition.x) * 2, boxCollider.size.y, boxCollider.size.z);
        }
	}
    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log(collider.name);
    }
}
