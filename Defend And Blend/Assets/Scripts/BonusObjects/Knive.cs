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

        //Debug.Log()
	}
    protected override void OnCollisionEnter(Collision collision)
    {
        Monster monster = collision.collider.gameObject.GetComponent<Monster>();//Get Monster
        if(monster != null)//Got monster
        {
            health -= 1;//monster.damage;
            HelpingHand helpingHand = GameObject.FindObjectOfType<HelpingHand>();
            helpingHand.holdingObject = null;
            helpingHand.isHoldingObject = false;
            monster.Die();
        }
        /*
        if (gameObject.rigidbody != null)
        {
            if (rigidbody.velocity.y == 0)
            {
                Destroy(gameObject.rigidbody);
            }
            Debug.Log(rigidbody.velocity);
        }
        */
        base.OnCollisionEnter(collision);
        //health
    }
}
