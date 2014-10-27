using UnityEngine;
using System.Collections;

public class Monster : Mover
{
    public Defendable target;//Target to walk tos
    public float damage;//Ammount of damage it does to the defendable target.
    public float attackSpeed;
    public float attackDistance;

    protected float nextAttack;
    protected bool isInAttackRange = false;
	// Use this for initialization
    protected override void Start()
    {
        base.Start();
    }
	// Update is called once per frame
    protected override void Update() 
    {
        if (target != null && !isInAttackRange)//If we have a target
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, 0);//Position to walk to
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);//Move torwards the target
        }
        Attack();
        base.Update();
	}
    protected virtual void Attack()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < attackDistance)
        {
            //http://docs.unity3d.com/ScriptReference/Time-time.html
            if (Time.time >= nextAttack)//Is it time to attack?
            {
                nextAttack = Time.time + attackSpeed;//Set up next attack
                target.DoDamage(damage);//Do Attack
            }
            isInAttackRange = true;
        }
        else
        {
            isInAttackRange = false;
            nextAttack = Time.time + attackSpeed; //Setup next attack until we are at our target
        }
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {        
        Defendable defendable = collision.gameObject.GetComponent<Defendable>();//Get Defendable Collision
        if (defendable != null)//If we collide with the defendable?
        {

        }
    }
}
