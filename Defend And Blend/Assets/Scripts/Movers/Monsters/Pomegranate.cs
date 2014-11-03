using UnityEngine;
using System.Collections;

public class Pomegranate : Monster 
{

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void Attack()
    {
        //There is No Attack
        //base.Attack();
    }
    protected override void OnCollisionEnter(Collision collision)
    {
#if !EXPLODE_IMPACT
        Defendable defendable = collision.gameObject.GetComponent<Defendable>();//Get Defendable Collision
        if (defendable != null)//If we collide with the defendable?
        {
            //TODO Sexy Explosion Particle
            defendable.DoDamage(damage);//Damage the defendable
            Die();
            //GameObject.Instantiate(explosionEffect,transform.position,transform.rotation);
            //Destroy(this.gameObject);
        }
#endif
        base.OnCollisionEnter(collision);
    }
}
