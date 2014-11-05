using UnityEngine;
using System.Collections;

public class BananaPeel : BonusObject 
{
    public float stunTime;//How long does it stun a monster?
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

    protected override void OnCollisionEnter(Collision collision)
    {
        Monster monster = collision.collider.gameObject.GetComponent<Monster>();//Get Monster
        if(monster != null)//Got monster
        {
            monster.Stun(stunTime);//Stun with a specific time
            Destroy(this.gameObject);//Destroy the peel
        }
        base.OnCollisionEnter(collision);
    }

}
