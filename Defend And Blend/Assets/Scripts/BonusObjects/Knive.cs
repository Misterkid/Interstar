using UnityEngine;
using System.Collections;

public class Knive : BonusObject 
{
    public float health = 3;
	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(health <= 0)
        {
            Destroy(this.gameObject);
        }
	}
    protected override void OnCollisionEnter(Collision collision)
    {
        Monster monster = collision.collider.gameObject.GetComponent<Monster>();//Get Monster
        if(monster != null)//Got monster
        {
            health -= 1;//monster.damage;
            monster.Die();
        }
        base.OnCollisionEnter(collision);
        //health
    }
}
