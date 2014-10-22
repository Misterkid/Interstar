using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour 
{
    public float speed = 25f;
    private PlayerShip playerShip;
	// Use this for initialization
	void Start () 
    {
        playerShip = GameObject.FindObjectOfType<PlayerShip>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //transform.Translate((transform.forward * -speed) * Time.deltaTime);
        if(playerShip.transform.position.z - 5 > transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, playerShip.transform.position.z + 50);
        }
	}
}
