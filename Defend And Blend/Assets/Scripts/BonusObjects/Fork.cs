using UnityEngine;
using System.Collections;

public class Fork : BonusObject 
{
    private float stunTime = 5;
    public float removeTime = 5;
    private float removeTimeEnd;//Did the stun timer end?
    public bool started = false;
	// Use this for initialization
	void Start ()
    {
        stunTime = removeTime;
        //removeTimeEnd = Time.time + removeTime;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (started)
        {
            if (Time.time >= removeTimeEnd)//stay stunned until stun time ends
            {
                Destroy(this.gameObject);
            }
        }
	}
    protected override void OnCollisionEnter(Collision collision)
    {
        Monster monster = collision.collider.gameObject.GetComponent<Monster>();//Get Monster
        if (monster != null)//Got monster
        {
            monster.Stun(stunTime);//Stun with a specific time
            if (!started)
            {
                started = true;
                removeTimeEnd = Time.time + removeTime;
            }
        }
        base.OnCollisionEnter(collision);
    }
}
