using UnityEngine;
using System.Collections;

public class Monster : Mover
{
    public Defendable target;
	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(target != null )
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, 0);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
	}
}
