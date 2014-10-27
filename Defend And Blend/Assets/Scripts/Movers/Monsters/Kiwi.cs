using UnityEngine;
using System.Collections;

public class Kiwi : Monster 
{
    public GameObject bulletParticle;
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
                    target.DoDamage(damage);//Do Attack
                    //particle
                    Quaternion quaternion = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                    quaternion.SetEulerAngles(transform.rotation.eulerAngles.x, transform.rotation.y - 90, transform.rotation.z);
                    GameObject.Instantiate(bulletParticle, transform.position, quaternion);
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
}
