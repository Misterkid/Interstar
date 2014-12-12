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
    public bool useController = true;

    public Animation handAnimation;

    private GameObject targetMonster;
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
        UDPInputController.Instance.OnInput += OnInputReceived;
	}
    private void OnDestroy()
    {
        //NotificationCenter.RemoveObserver(LogController.EventRequestFrameData, OnRequestFrameData);

        //if (GamepadController.Instance != null)
           // GamepadController.Instance.OnInput -= OnInputReceived;

        if (UDPInputController.Instance != null)
            UDPInputController.Instance.OnInput -= OnInputReceived;
    }

    private void OnInputReceived(float input1, float input2)
    {
        if (!useController)
        {
            float normalizedInput1 = CalibrationSettings.GetNormalizedValue(Side.Left, input1);
            float normalizedInput2 = CalibrationSettings.GetNormalizedValue(Side.Right, input2);
            //Open/Close
            Squeezing(normalizedInput1, normalizedInput2);
        }
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
        float openPressure = Input.GetAxis("RTrigger");
        float closePressure = Input.GetAxis("LTrigger");
        if (useController)
            Squeezing(openPressure, closePressure);
        //While holding a object check if the pressure is below minumum if so let it go.
        // Also check if the pressure is above maximum if so Kill the holding object.
        if (isHoldingObject)
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
    private void VisualSqueeze(float squeezePressure)
    {
        //squeezePressure = 0 ~ 100%
        //maxPressure * squeezePressure = ?? / 100 = position
        float newRightX = ((maxPressure) * squeezePressure) / 100;   //rightObject.transform.localPosition.x
        float newLeftX = -((maxPressure) * squeezePressure) / 100;
        rightObject.transform.localPosition = new Vector3(1 - newRightX, rightObject.transform.localPosition.y, rightObject.transform.localPosition.z);
        leftObject.transform.localPosition = new Vector3(-1 - newLeftX, leftObject.transform.localPosition.y, leftObject.transform.localPosition.z);
        Debug.Log(leftObject.transform.localPosition.x);
    }
    private void Squeezing(float openPressure,float closePressure)
    {
        if (rightObject != null && leftObject != null)
        {
            if (openPressure > 0 && rightObject.transform.localPosition.x < maxPressure)
            {
                rightObject.transform.localPosition = new Vector3(rightObject.transform.localPosition.x + (openPressure * Time.deltaTime), rightObject.transform.localPosition.y, rightObject.transform.localPosition.z);
                leftObject.transform.localPosition = new Vector3(leftObject.transform.localPosition.x - (openPressure * Time.deltaTime), leftObject.transform.localPosition.y, leftObject.transform.localPosition.z);
            }
            else if (closePressure > 0 && rightObject.transform.localPosition.x > minPressure)
            {
                rightObject.transform.localPosition = new Vector3(rightObject.transform.localPosition.x - (closePressure * Time.deltaTime), rightObject.transform.localPosition.y, rightObject.transform.localPosition.z);
                leftObject.transform.localPosition = new Vector3(leftObject.transform.localPosition.x + (closePressure * Time.deltaTime), leftObject.transform.localPosition.y, leftObject.transform.localPosition.z);
            }

            BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>() as BoxCollider;
            boxCollider.size = new Vector3((rightObject.transform.localPosition.x) * 2, boxCollider.size.y, boxCollider.size.z);
            squeezePressure = (maxPressure * 2) - Vector3.Distance(rightObject.transform.localPosition, leftObject.transform.localPosition);

            squeezePressure = squeezePressure / 2;//maxPressure/squeezePressure ;

            float animationTime = ((handAnimation["Squeeze"].length) / 100) * (squeezePressure * 100);
            handAnimation["Squeeze"].time = handAnimation["Squeeze"].length - animationTime;

            if (!handAnimation.isPlaying)
            {
                if (handAnimation.clip.name == "Squeeze" && rightObject.transform.localPosition.x > maxPressure)
                    handAnimation.Play("Shake");
                else
                    handAnimation.Play("Squeeze");
            }
        }
    }
    private void AutoMove()
    {
        if((AutoMoveX || AutoMoveY )&& !isHoldingObject)
        {
            //bool isAboveBlender = false;
            Monster monster = null;
            
           // GameObject monsterGo = null;
            if(targetMonster == null)
                targetMonster = EUtils.GetNearestObject(waveSpawner.SpawnedMonsters, transform.position);

            RaycastHit hit;
            if (targetMonster != null)
            {
                BlenderCatch blender = GameObject.FindObjectOfType<BlenderCatch>();

                Ray ray = new Ray(targetMonster.transform.position, -Vector3.up);
                if (blender.collider.Raycast(ray,out hit,float.MaxValue))
                {
                    targetMonster = null;
                    return;
                }
                monster = targetMonster.GetComponent<Monster>();
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
            if(AutoGrab)
            {
                if (monster != null)
                {
                    if (!monster.isInBlender && Vector3.Distance(transform.position, monster.transform.position) < 1)
                    {
                        VisualSqueeze(monster.minPressure + 1);
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
            if (AutoGrab)
            {
                if (blender != null)
                {
                     if (transform.position.y == blender.transform.position.y + 7.50f && transform.position.x == blender.transform.position.x)
                     {
                         Debug.Log("Hello");
                         VisualSqueeze(0);
                         /*
                         holdingObject.transform.localPosition = new Vector3(holdingObject.pickUpHandPosition.x, holdingObject.pickUpHandPosition.y, 0);
                         holdingObject.transform.parent = null;
                         isHoldingObject = false;
                         holdingObject.LetGo();
                         holdingObject.Stun(1);
                         holdingObject = null;
                         targetMonster = null;
                          */ 
                     }
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