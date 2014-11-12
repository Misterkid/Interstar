using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BlenderCatch : MonoBehaviour 
{
    private List<Monster> monsters = new List<Monster>();
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetButton("Fire1"))
        {
            for(int i = 0; i < monsters.Count; i++)
            {
                monsters[i].Die();
                monsters[i] = null;
                GameValues.SCORE++;
            }
            monsters = new List<Monster>();
        }
	}
    void OnCollisionEnter(Collision other)
    {
        Monster monster = other.collider.GetComponent<Monster>();
        if (monster != null)
        {
            if (!monster.isInholding)
            {
                GameValues.SCORE++;
                monsters.Add(monster);
                //monster.Die();
            }
        }
    }
}
