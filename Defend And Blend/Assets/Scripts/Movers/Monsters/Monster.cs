using UnityEngine;
using System.Collections;

public class Monster : Mover
{
    public Defendable target;//Target to walk tos
    public float damage;//Ammount of damage it does to the defendable target.
    public float attackSpeed;
    public float attackDistance;
    public float minPressure = 10;
    public float maxPressure = 80;

    public GameObject explosionEffect;//Explosion particle

    protected float nextAttack;
    protected bool isInAttackRange = false;

    protected float stunTime;
    protected bool isStunned = false;
    protected float stunTimeEnd;

    protected bool hasSpeedBoost = false;//can only be boosted once.
    protected bool isInholding = false;
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
        if (!isInholding)
        {
            if (!isStunned)
            {
                if (target != null && !isInAttackRange)//If we have a target and are not stunned
                {
                    Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, 0);//Position to walk to
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);//Move torwards the target
                    transform.LookAt(targetPosition);
                }
                Attack();
            }
            Stunned();
            base.Update();
        }
	}
    public virtual void Hold()
    {
        rigidbody.isKinematic = true;
        isInholding = true;
    }
    public virtual void LetGo()
    {
        rigidbody.isKinematic = false;
        isInholding = false;
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
        RaycastHit[] hit;
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
                foundHit = true;
            }
        }
        if (!foundHit)
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
#if EXPLODE_IMPACT
            //TODO Sexify Explosion Particle
            defendable.DoDamage(damage);//Damage the defendable
            Die();//destroy
#endif
        }
    }
    public void Die()
    {
        GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
