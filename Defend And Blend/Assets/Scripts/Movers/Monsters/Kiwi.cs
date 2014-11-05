using UnityEngine;
using System.Collections;

public class Kiwi : Monster 
{
    public Bullet bullet;//Kiwi bullet
    public AudioClip bulletAudioClip;//Bullet sound
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
        if(isInAttackRange)//Are we in range?
        {
            //Object distance
            float distance = Vector3.Distance(transform.position, target.transform.position);
            //is the current distance lower then the attack distance?
            if (distance < attackDistance)
            {
                //http://docs.unity3d.com/ScriptReference/Time-time.html
                if (Time.time >= nextAttack)//Is it time to attack?
                {
                    nextAttack = Time.time + attackSpeed;//Set up next attack
                    Bullet clone = GameObject.Instantiate(bullet, transform.position, transform.rotation) as Bullet;//Clone bullet prefeb
                    SoundManager.PlaySound(bulletAudioClip, transform.position, SoundManager.SoundTypes.EFFECT, false, transform);//Play Sound at some position with soundtype of Effect  not looping and parent of this gameobject.
                    clone.Shoot(damage, target);//The bullet goes torwards the target

                }
                isInAttackRange = true;//We are in range
            }
            else
            {
                isInAttackRange = false;// Not anymore
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
