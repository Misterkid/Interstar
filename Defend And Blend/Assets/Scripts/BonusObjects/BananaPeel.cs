using UnityEngine;
using System.Collections;

public class BananaPeel : BonusObject 
{
    public float stunTime;
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
        Monster monster = collision.collider.gameObject.GetComponent<Monster>();
        if(monster != null)
        {
            monster.Stun(stunTime);
            Destroy(this.gameObject);
        }
        base.OnCollisionEnter(collision);
    }

}
