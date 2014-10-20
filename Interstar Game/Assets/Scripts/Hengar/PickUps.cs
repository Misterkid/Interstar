using UnityEngine;
using System.Collections;

public class PickUps : MonoBehaviour 
{
    public int minPressure = 0;
    public int maxPressure = 100;
    public string objectName = "Null";
    public bool isGrounded = false;
	// Use this for initialization
	void Start () 
    {
        if (minPressure < 0)
        {
            Debug.Log(string.Format("Min Pressure is {0} and is now set to 1.", minPressure));
            minPressure = 1;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
            isGrounded = true;
    }
}
