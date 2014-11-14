using UnityEngine;
using System.Collections;

public class Kiwi : Monster 
{
    public Bullet bullet;//Kiwi bullet
    public AudioClip bulletAudioClip;//Bullet sound
    /*
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
    */
    protected override void Attack()
    {
        if(isInAttackRange)//Are we in range?
        {
            //Object distance
            //Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, 0);
            //float distance = Vector3.Distance(transform.position, targetPosition);
            //is the current distance lower then the attack distance?
           // Debug.Log(Mathf.Abs(transform.position.x - target.transform.position.x));
            //Debug.Log(distance + ":" + attackDistance);

            //if (distance <= attackDistance)
            //{
               // Debug.Log(Time.time + ":" + nextAttack);
                //http://docs.unity3d.com/ScriptReference/Time-time.html
                if (Time.time >= nextAttack)//Is it time to attack?
                {
                    //Debug.Log("BOOM");
                    nextAttack = Time.time + attackSpeed;//Set up next attack
                    Bullet clone = GameObject.Instantiate(bullet, transform.position, transform.rotation) as Bullet;//Clone bullet prefeb
                    
                    SoundManager.PlaySound(bulletAudioClip, transform.position, SoundManager.SoundTypes.EFFECT, false, transform);//Play Sound at some position with soundtype of Effect  not looping and parent of this gameobject.
                    clone.Shoot(damage, target);//The bullet goes torwards the target\

                }
                isInAttackRange = true;//We are in range
           // }
            //else
            //{
                //Debug.Log("NOPE.AVI");
                //isInAttackRange = false;// Not anymore
                //nextAttack = Time.time + attackSpeed; //Setup next attack until we are at our target
            //}
            //spawn particle
        }
        else
        {
            //isInAttackRange = false;// Not anymore
            nextAttack = Time.time + attackSpeed; //Setup next attack until we are at our target
        }
        base.Attack();
    }
    /*
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
     */
}
