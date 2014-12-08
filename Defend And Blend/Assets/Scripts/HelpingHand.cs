using UnityEngine;
using System.Collections;
public class HelpingHand : MonoBehaviour 
{
    public GameObject rightObject;
    public GameObject leftObject;
   // public float holdHeight;
    public float maxHeight;
    public float minHeight;
    public float maxPressure = 1;
    public float minPressure = 0;

    public bool AutoMoveX = false;
    public bool AutoMoveY = false;
    public bool AutoGrab = false;


    public Animation handAnimation;
    private WaveSpawnerTwo waveSpawner;
    private bool isHoldingObject = false;
    private Monster holdingObject;
    private float squeezePressure;
	// Use this for initialization
	void Start () 
    {
        waveSpawner = FindObjectOfType<WaveSpawnerTwo>();
        handAnimation["Squeeze"].enabled = true;
        handAnimation["Squeeze"].weight = 1f;
        handAnimation["Squeeze"].time = handAnimation["Squeeze"].length;
        handAnimation["Squeeze"].speed = 0f;
        //Debug.Log("Time: " + handAnimation["Squeeze"].time + " : " + handAnimation["Squeeze"].length);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    //Controls Auto
        AutoMove();

        transform.Translate((Input.GetAxis("Horizontal") * 10) * Time.deltaTime, (Input.GetAxis("Vertical") * 10) * Time.deltaTime, 0);

        if (transform.position.y < minHeight)
        {
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
        }
        if(transform.position.y > maxHeight)
        {
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
        }
        //pressure
        if(rightObject != null && leftObject != null)
        {
            float openPressure = Input.GetAxis("RTrigger");
            float closePressure = Input.GetAxis("LTrigger");
            if (openPressure > 0 && rightObject.transform.localPosition.x < maxPressure)
            {
                rightObject.transform.localPosition = new Vector3(rightObject.transform.localPosition.x + (openPressure * Time.deltaTime), rightObject.transform.localPosition.y, rightObject.transform.localPosition.z);
                leftObject.transform.localPosition = new Vector3(leftObject.transform.localPosition.x - (openPressure * Time.deltaTime), leftObject.transform.localPosition.y, leftObject.transform.localPosition.z);

             }
            else if(closePressure > 0 && rightObject.transform.localPosition.x > minPressure)
            {
                rightObject.transform.localPosition = new Vector3(rightObject.transform.localPosition.x - (closePressure * Time.deltaTime), rightObject.transform.localPosition.y, rightObject.transform.localPosition.z);
                leftObject.transform.localPosition = new Vector3(leftObject.transform.localPosition.x + (closePressure * Time.deltaTime), leftObject.transform.localPosition.y, leftObject.transform.localPosition.z);
            }
            else
            {

            }

            BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>() as BoxCollider;
            boxCollider.size = new Vector3( (rightObject.transform.localPosition.x) * 2, boxCollider.size.y, boxCollider.size.z);
            squeezePressure = (maxPressure * 2) - Vector3.Distance(rightObject.transform.localPosition, leftObject.transform.localPosition);

            squeezePressure = squeezePressure / 2;//maxPressure/squeezePressure ;

            float animationTime = ((handAnimation["Squeeze"].length ) / 100) * (squeezePressure * 100);
            handAnimation["Squeeze"].time = handAnimation["Squeeze"].length - animationTime;

            if (!handAnimation.isPlaying)
                handAnimation.Play("Squeeze");
        }
        //While holding a object check if the pressure is below minumum if so let it go.
        // Also check if the pressure is above maximum if so Kill the holding object.
        if(isHoldingObject)
        {
            if (holdingObject != null && !holdingObject.isInBlender)
            {
                if (squeezePressure * 100 < holdingObject.minPressure)
                {
                    holdingObject.transform.localPosition = new Vector3(holdingObject.pickUpHandPosition.x, holdingObject.pickUpHandPosition.y, 0);

                    holdingObject.transform.parent = null;
                    isHoldingObject = false;
                    holdingObject.LetGo();
                    holdingObject.Stun(1);
                    holdingObject = null;
                }
                else if (squeezePressure * 100 > holdingObject.maxPressure)
                {
                    isHoldingObject = false;
                    holdingObject.Die();
                    GameValues.SCORE--;
                    holdingObject = null;
                }
            }
        }
        //Debug.Log(squeezePressure * 100);
	}
    private void AutoMove()
    {
        if((AutoMoveX || AutoMoveY )&& !isHoldingObject)
        {
            Monster monster = null;
            GameObject monsterGo = null;
            monsterGo = EUtils.GetNearestObject(waveSpawner.SpawnedMonsters, transform.position);//EUtils.GetNearestObjectOfType<Monster>(transform.position);
            if (monsterGo != null)
            {
                monster = monsterGo.GetComponent<Monster>();
                if(monster.isInBlender)
                    monsterGo = EUtils.GetNearestObject(waveSpawner.SpawnedMonsters, transform.position, monsterGo);
                
                if (monsterGo != null)
                    monster = monsterGo.GetComponent<Monster>();
            }

            if (AutoMoveX)
            {
                if (monster != null)
                {
                    if (!monster.isInBlender)
                    {
                        Vector3 targetPosition = new Vector3(monster.transform.position.x, transform.position.y, transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);
                    }
                }
            }
            if (AutoMoveY)
            {
                if (monster != null)
                {
                    if (!monster.isInBlender && transform.position.x == monster.transform.position.x)
                    {
                        Vector3 targetPosition = new Vector3(transform.position.x, monster.transform.position.y, transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);
                    }
                }
            }
        }
        else if((AutoMoveX || AutoMoveY )&& isHoldingObject)
        {
            BlenderCatch blender = GameObject.FindObjectOfType<BlenderCatch>();//EUtils.GetNearestObject(waveSpawner.SpawnedMonsters, transform.position);
            if (AutoMoveX)
            {
                if (blender != null && transform.position.y == blender.transform.position.y + 7.50f)
                {
                    Vector3 targetPosition = new Vector3(blender.transform.position.x, transform.position.y, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);
                }
            }
            if (AutoMoveY)
            {
                if (blender != null)
                {
                    Vector3 targetPosition = new Vector3(transform.position.x, blender.transform.position.y + 7.50f, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);
                }
            }
        }
    }
    //Hold the monster when the pressure is above the minimum.
    private void OnTriggerStay(Collider other)
    {
        if (!isHoldingObject)
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null && !monster.isInBlender)
            {
                if (squeezePressure * 100 > monster.minPressure)
                {
                    monster.transform.parent = this.transform;
                    monster.Hold();
                    //monster.transform.localPosition = new Vector3(0, holdHeight, 0);

                    monster.transform.localPosition = monster.pickUpHandPosition;
                    /*
                    Banana banana = other.GetComponent<Banana>();
                    if (banana != null)
                        banana.DropPeel();
                    */
                    isHoldingObject = true;
                    holdingObject = monster;
                }
            }
        }
    }
}