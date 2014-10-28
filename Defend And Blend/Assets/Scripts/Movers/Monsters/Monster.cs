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

    protected float stunTime;
    protected bool isStunned = false;
    protected float stunTimeEnd;

    protected bool hasSpeedBoost = false;//can only be boosted once.
	// Use this for initialization
    protected override void Start()
    {
        IgnoreCollision();
        attackDistance = attackDistance + (EUtils.GetObjectCollUnitSize(gameObject).z / 2);
        if (attackDistance <= 0)
            attackDistance = 0.1f;

        base.Start();
    }
    protected virtual void IgnoreCollision()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Monster");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i] != gameObject)
                Physics.IgnoreCollision(gameObject.collider,gameObjects[i].collider,true);
        }
    }
	// Update is called once per frame
    protected override void Update() 
    {
        if (!isStunned)
        {
            if (target != null && !isInAttackRange)//If we have a target and are not stunned
            {
                Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, 0);//Position to walk to
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);//Move torwards the target
            }
            Attack();
        }
        Stunned();
        base.Update();
	}
    public virtual void BoostSpeed(float speedBoost)
    {
        if (!hasSpeedBoost)
        {
            speed += speedBoost;
            hasSpeedBoost = true;
        }
    }
    protected virtual void Stunned()
    {
        if (isStunned)
        {
            if (Time.time >= stunTimeEnd)
            {
                isStunned = false;
            }
        }
    }
    public virtual void Stun(float time)
    {
        isStunned = true;
        stunTime = time;
        stunTimeEnd = Time.time + stunTime;
    }
    protected virtual void Attack()
    {
        //if(Physics.Raycast)
        RaycastHit[] hit;// = new RaycastHit();
        bool foundHit = false;
        hit =  Physics.RaycastAll(transform.position, transform.forward * 0.5f, attackDistance);
        for(int i = 0; i < hit.Length; i++)
        {
            if(hit[i].collider == target.collider)
            {
                //http://docs.unity3d.com/ScriptReference/Time-time.html
                if (Time.time >= nextAttack)//Is it time to attack?
                {
                    nextAttack = Time.time + attackSpeed;//Set up next attack
                    target.DoDamage(damage);//Do Attack
                }
                isInAttackRange = true;
                Debug.Log("hit");
                foundHit = true;
            }
        }
        if (!foundHit)
        {
            Debug.Log("reset");
            isInAttackRange = false;
            nextAttack = Time.time + attackSpeed; //Setup next attack until we are at our target
        }
        /*
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= attackDistance)
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
         */
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {    
        Defendable defendable = collision.gameObject.GetComponent<Defendable>();//Get Defendable Collision
        if (defendable != null)//If we collide with the defendable?
        {

        }
    }
}
