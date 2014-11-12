using UnityEngine;
using System.Collections;

public class Orange : Monster 
{
    public float timeToStun;
    // Use this for initialization
    /*
    protected override void Start()
    {
        base.Start();
    }
    protected override void IgnoreCollision()
    {
        //Orange should not ignore collision until it collided!
        //base.IgnoreCollision();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    */
    protected override void OnCollisionEnter(Collision collision)
    {
        Monster otherMonster = collision.gameObject.GetComponent<Monster>();//Get Defendable Collision
        if (otherMonster != null)//If we collide with the defendable?
        {
            otherMonster.Stun(timeToStun);//Stun the monster
            Physics.IgnoreCollision(gameObject.collider, otherMonster.collider);
        }
        base.OnCollisionEnter(collision);
    }
}
