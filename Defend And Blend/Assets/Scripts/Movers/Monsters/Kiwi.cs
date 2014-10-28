using UnityEngine;
using System.Collections;

public class Kiwi : Monster 
{
    public Bullet bullet;
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
        if(isInAttackRange)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < attackDistance)
            {
                //http://docs.unity3d.com/ScriptReference/Time-time.html
                if (Time.time >= nextAttack)//Is it time to attack?
                {
                    nextAttack = Time.time + attackSpeed;//Set up next attack
                    //target.DoDamage(damage);//Do Attack
                    //bullet
                    /*
                    Quaternion quaternion = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                    quaternion.SetEulerAngles(transform.rotation.eulerAngles.x, transform.rotation.y - 90, transform.rotation.z);
                    */
                    Bullet clone = GameObject.Instantiate(bullet, transform.position, transform.rotation) as Bullet;
                    clone.Shoot(damage, target);
                }
                isInAttackRange = true;
            }
            else
            {
                isInAttackRange = false;
                nextAttack = Time.time + attackSpeed; //Setup next attack until we are at our target
            }
            //spawn particle
        }
        base.Attack();
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
