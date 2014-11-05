using UnityEngine;
using System.Collections;

public class BlenderCatch : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    void OnTriggerEnter(Collider other)
    {
        Monster monster = other.GetComponent<Monster>();
        if (monster != null)
        {
            if(!monster.isInholding)
                monster.Die();
        }
    }
}
