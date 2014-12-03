using UnityEngine;
using System.Collections;

public class Monster : Mover
{

    public Defendable target;//Target to walk tos
    public float damage;//Ammount of damage it does to the defendable target.
    public float attackSpeed;//attacking speed
    public float attackDistance;//Attacking distance
    public float minPressure = 10;//Minumum pressure under this pressure the object falls
    public float maxPressure = 80;//Maximum pressure above this pressure this object dies
    public bool isInholding = false;//do we hold this object?
    public GameObject explosionEffect;//Explosion particle
    public AudioClip stunnedClip;
    public bool isInBlender = false;
    public Color explosionColor;

    public Vector3 pickUpHandPosition = Vector3.zero;
    protected float nextAttack;//The next attack.
    protected bool isInAttackRange = false;//Are we in range?

    private float stunTime;//How long are we stunned?
    private bool isStunned = false;//are we stunned
    private float stunTimeEnd;//Did the stun timer end?
    private bool hasSpeedBoost = false;//can only be boosted once.
    private float animationSpeed;
    private Animator animator;
	// Use this for initialization
    protected override void Start()
    {
        IgnoreCollision();//Ignore all collisions with the same tag
        attackDistance = attackDistance + (EUtils.GetObjectCollUnitSize(gameObject).z / 2);
        if (attackDistance <= 0)
            attackDistance = 0.1f;

        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animationSpeed = animator.speed;
        }
        base.Start();
    }
    private void IgnoreCollision()
    {
        //Ignore all collisions with the same tag
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Monster");//get all gameobject with the tag Monster
        for (int i = 0; i < gameObjects.Length; i++)//Come on....
        {
            if (gameObjects[i] != gameObject)//its not the same object as this one
                Physics.IgnoreCollision(gameObject.collider,gameObjects[i].collider,true);//ignore this object with another object
        }
    }
	// Update is called once per frame
    protected override void Update() 
    {
        if (GameValues.ISPAUSED)
            return;

        if (!isInholding && !isInBlender)//Is this not in holding?
        {
            if (!isStunned)//is it not stunned?
            {
#if EXPLODE_IMPACT
                //if (target != null)//If we don't  have a target
                if (target != null && !isInAttackRange)
#else
                if (target != null && !isInAttackRange)//If we don't  have a target and arn't in range
#endif
                {
                    Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, 0);//Position to walk to
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);//Move torwards the target
                    transform.LookAt(targetPosition);//look at the target position
                }
                Attack();//ATTACK!
            }
            Stunned();//Check for stun.
            base.Update();//Base update
        }
	}
    public void Hold()//Hold object!
    {
        if (!isInBlender)
        {
            animator.speed = 0f;
            rigidbody.isKinematic = true;//No gravity and such
            isInholding = true;//we are holding this now
        }
    }
    public void LetGo()//Let go!
    {
        rigidbody.isKinematic = false;//Lets turn it back off
        isInholding = false;//we arn't holding it anymore
    }
    public void BoostSpeed(float speedBoost)
    {
        if (!hasSpeedBoost)//We dont have a speedboost
        {
            speed += speedBoost;//Get more speed
            hasSpeedBoost = true;//speed bost accuired
        }
    }
    private void Stunned()//STUN check
    {
        if (isStunned)//are we stunned?
        {
            if (Time.time >= stunTimeEnd)//stay stunned until stun time ends
            {
                isStunned = false;//we arn't stunned anymore
                if (animator != null)
                {
                    animator.speed = 1;
                }
            }
        }
    }
    public virtual void Stun(float time)//STUN with a specific time
    {
        if (stunnedClip != null)
            SoundManager.Instance.PlaySound(stunnedClip, transform.position, SoundManager.SoundTypes.EFFECT, false, transform);

        if(animator != null)
        {
            animator.speed = 0;
        }

        isStunned = true;//stunned
        stunTime = time;//time we are stunning
        stunTimeEnd = Time.time + stunTime;//the stunning end time.
    }
    protected virtual void Attack()//Attack!
    {
        RaycastHit[] hit;//Get all objects that we hit
        bool foundHit = false;//found a target to hit
        Vector3 castPosition = new Vector3(transform.position.x, transform.position.y + (EUtils.GetObjectCollUnitSize(gameObject).y), transform.position.z);
        hit =  Physics.RaycastAll(castPosition, transform.forward /* * 0.5f*/, attackDistance);
        //Debug.DrawRay(castPosition, transform.forward * attackDistance, Color.red);
       // Debug.Log(hit.Length);
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
            //TODO improve Explosion Particle
            defendable.DoDamage(damage);//Damage the defendable
            GameValues.SCORE--;
            Die();//destroy
#endif
        }
    }
    public void Die()
    {
        GameObject clone = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation) as GameObject;
        Explosion explosion = clone.GetComponent<Explosion>();
        explosion.explode(explosionColor);
        //We are gone!
        WaveSpawnerTwo waveSpawner = FindObjectOfType<WaveSpawnerTwo>();
        waveSpawner.SpawnedMonsters.Remove(this.gameObject);

        Destroy(this.gameObject);
    }
}
