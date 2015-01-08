using UnityEngine;
using System.Collections;
//using System;
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
    public bool useStick = true;
    public Animation handAnimation;

    public GameObject knifeBonusBtn;
    public GameObject forkBonusBtn;

    public BonusObject knifeBonus;
    public BonusObject forkBonus;
    public GameObject holdingBonusObject;


    private GameObject targetMonster;
    private WaveSpawnerTwo waveSpawner;
    [HideInInspector]
    public bool isHoldingObject = false;
    [HideInInspector]
    public Monster holdingObject;

    private float squeezePressure;

    //Jildert logging
    private float lastInput1;
    private float lastInput2;
    private float lastNormalizedInput1;
    private float lastNormalizedInput2;
	// Use this for initialization
	void Start () 
    {
        waveSpawner = FindObjectOfType<WaveSpawnerTwo>();
        handAnimation["Squeeze"].enabled = true;
        handAnimation["Squeeze"].weight = 1f;
        handAnimation["Squeeze"].time = handAnimation["Squeeze"].length;
        handAnimation["Squeeze"].speed = 0f;
        if (!useController)
            UDPInputController.Instance.OnInput += OnInputReceived;

        //Jildert logging
        NotificationCenter.AddObserver(LogController.EventRequestFrameData, OnRequestFrameData);
	}
    //Jildert logging
    private void OnRequestFrameData(Notification notification)
    {
        // add all frame data
        LogDataModel logModel = notification.Sender as LogDataModel;
        if (logModel != null)
        {
            logModel.SetValue(LogFrameDataColumns.LeftRaw.ToString(), lastInput1.ToString("f3"));
            logModel.SetValue(LogFrameDataColumns.RightRaw.ToString(), lastInput2.ToString("f3"));
            logModel.SetValue(LogFrameDataColumns.LeftNormalized.ToString(), lastNormalizedInput1.ToString("f3"));
            logModel.SetValue(LogFrameDataColumns.RightNormalized.ToString(), lastNormalizedInput2.ToString("f3"));
            logModel.SetValue(LogFrameDataColumns.HandPositionX.ToString(), transform.position.x.ToString("f3"));
            logModel.SetValue(LogFrameDataColumns.HandPositionY.ToString(), transform.position.x.ToString("f3"));

            logModel.SetValue(LogFrameDataColumns.Pressure.ToString(),squeezePressure.ToString("f3"));

            if (holdingObject != null)
            {
                logModel.SetValue(LogFrameDataColumns.HoldingObjectName.ToString(), holdingObject.name);
                logModel.SetValue(LogFrameDataColumns.MinObjectPressure.ToString(), holdingObject.minPressure.ToString("f3"));
                logModel.SetValue(LogFrameDataColumns.MaxObjectPressure.ToString(), holdingObject.maxPressure.ToString("f3"));
            }
            else
            {
                logModel.SetValue(LogFrameDataColumns.HoldingObjectName.ToString(), "No Object");
                logModel.SetValue(LogFrameDataColumns.MinObjectPressure.ToString(), "0");
                logModel.SetValue(LogFrameDataColumns.MaxObjectPressure.ToString(), "100");
            }
        }
    }

    private void OnDestroy()
    {
        if (UDPInputController.Instance != null)
            UDPInputController.Instance.OnInput -= OnInputReceived;
        //Jildert logging
        NotificationCenter.RemoveObserver(LogController.EventRequestFrameData, OnRequestFrameData);
    }

    private void OnInputReceived(float input1, float input2)
    {
        if (!useController)
        {
            lastInput1 = input1;
            lastInput2 = input2;
            float normalizedInput1 = CalibrationSettings.GetNormalizedValue(Side.Left, input1);
            float normalizedInput2 = CalibrationSettings.GetNormalizedValue(Side.Right, input2);
            lastNormalizedInput1 = normalizedInput1;
            lastNormalizedInput2 = normalizedInput2;
            //Open/Close
            Squeezing(normalizedInput1, normalizedInput2);
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (GameValues.ISPAUSED)
            return;//Do nothing while paused

        //Controls Auto
        AutoMove();
        //pressure
        float openPressure = Input.GetAxis("RTrigger");
        float closePressure = Input.GetAxis("LTrigger");
        if (!isHoldingObject && holdingBonusObject == null)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z + 2);
           // RaycastHit[] hit = Physics.RaycastAll(position, transform.forward /* * 0.5f*/, float.MaxValue);
            Ray ray = new Ray(position,transform.forward);
            RaycastHit hit;
            Debug.DrawRay(position, (transform.forward * 50), Color.red);

            Debug.Log((squeezePressure * 100 > 20 && squeezePressure * 100 < 50) + ":" + GameValues.SMOOTHYPOINTS);
            if ( squeezePressure * 100 > 20 && squeezePressure * 100 < 50)
            {
                if (Physics.Raycast(ray, out hit, 20))
                {
                    // Debug.Log(hit.collider);
                    if (hit.collider.gameObject == forkBonusBtn)
                    {
                        BlenderCatch blender = GameObject.FindObjectOfType<BlenderCatch>();
                        if (GameValues.SMOOTHYPOINTS >= 10)
                        {
                            holdingBonusObject = GameObject.Instantiate(forkBonus.gameObject) as GameObject;
                            holdingBonusObject.collider.enabled = false;
                            holdingBonusObject.transform.parent = this.transform;
                            holdingBonusObject.transform.position = transform.position;
                            isHoldingObject = true;
                            GameValues.SMOOTHYPOINTS -= 10;
                            blender.smoothyText.text = GameValues.SMOOTHYPOINTS.ToString();
                        }

                        //Debug.Log("fork");
                    }
                    else if (hit.collider.gameObject == knifeBonusBtn)
                    {
                        BlenderCatch blender = GameObject.FindObjectOfType<BlenderCatch>();
                        if (GameValues.SMOOTHYPOINTS >= 20)
                        {
                            holdingBonusObject = GameObject.Instantiate(knifeBonus.gameObject) as GameObject;
                            holdingBonusObject.collider.enabled = false;
                            holdingBonusObject.transform.parent = this.transform;
                            holdingBonusObject.transform.position = transform.position;
                            isHoldingObject = true;

                            GameValues.SMOOTHYPOINTS -= 20;
                            blender.smoothyText.text = GameValues.SMOOTHYPOINTS.ToString();
                        }
                        //Debug.Log("Knife");
                    }
                }
            }

        }
        else if (holdingBonusObject != null)
        {
            if (squeezePressure * 100 < 20 && holdingBonusObject != null)
            {
                holdingBonusObject.collider.enabled = true;
                holdingBonusObject.transform.parent = null;
                holdingBonusObject.AddComponent<Rigidbody>();
                //holdingBonusObject.transform.position = transform.position;
                holdingBonusObject = null;
                isHoldingObject = false;
            }
        }

        if (useStick)
        {
            transform.Translate((Input.GetAxis("Horizontal") * 10) * Time.deltaTime, (Input.GetAxis("Vertical") * 10) * Time.deltaTime, 0);
            if (transform.position.y < minHeight)
            {
                transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
            }
            if (transform.position.y > maxHeight)
            {
                transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
            }
        }
        else
        {
            if(!AutoMoveX)
            {
                if (openPressure > closePressure)
                    transform.Translate(openPressure * 10 * Time.deltaTime,0,0);
                else if( openPressure < closePressure)
                    transform.Translate(-closePressure * 10 * Time.deltaTime, 0, 0);
            }
            if(!AutoMoveY)
            {
                if (openPressure > closePressure)
                    transform.Translate(0,openPressure * 10 * Time.deltaTime, 0);
                else if (openPressure < closePressure)
                    transform.Translate(0,-closePressure * 10 * Time.deltaTime, 0);
            }
        }

        if (useController && !AutoGrab)
            Squeezing(openPressure, closePressure);
        //While holding a object check if the pressure is below minumum if so let it go.
        // Also check if the pressure is above maximum if so Kill the holding object.
        if (isHoldingObject)
        {
            if (holdingObject != null && !holdingObject.isInBlender)
            {
                if (squeezePressure * 100 < holdingObject.minPressure)
                {
                    //Raycast
                    bool blenderIsFull = false;
                   // Defendable defendAble = GameObject.FindObjectOfType<Defendable>();

                    RaycastHit[] hit = Physics.RaycastAll(holdingObject.transform.position, -Vector3.up /* * 0.5f*/, float.MaxValue);
                    for (int i = 0; i < hit.Length; i++)
                    {
                        BlenderCatch blenderCatch = hit[i].collider.GetComponent<BlenderCatch>();
                        if (blenderCatch != null)
                        {
                            if (blenderCatch.smoothPoints > blenderCatch.maxSmoothPoints)
                                blenderIsFull = true;

                            break;
                        }
                    }
                    if (!blenderIsFull)
                    {
                        holdingObject.transform.localPosition = new Vector3(holdingObject.pickUpHandPosition.x, holdingObject.pickUpHandPosition.y, 0);
                        holdingObject.transform.parent = null;
                        isHoldingObject = false;
                        holdingObject.LetGo();
                        holdingObject.Stun(1);
                        holdingObject = null;
                    }
                    else
                    {
                        holdingObject.transform.localPosition = new Vector3(holdingObject.pickUpHandPosition.x - 4, holdingObject.pickUpHandPosition.y, 0);
                        holdingObject.transform.parent = null;
                        isHoldingObject = false;
                        holdingObject.LetGo();
                        holdingObject.Stun(1);
                        holdingObject = null;
                        Debug.Log("Blender is full!!!!!!!!");
                    }
                }
                else if (squeezePressure * 100 > holdingObject.maxPressure)
                {

                    holdingObject.transform.localPosition = new Vector3(holdingObject.pickUpHandPosition.x, holdingObject.pickUpHandPosition.y, 0);
                    holdingObject.transform.parent = null;
                    isHoldingObject = false;
                    holdingObject.Die();
                    holdingObject = null;
                    GameValues.SCORE--;
                }
            }
        }
        //Debug.Log(squeezePressure * 100);
	}
    private void VisualSqueeze(float pressure)
    {
        squeezePressure = pressure / 100;
        float newRightX = ((maxPressure) * pressure) / 100;   //rightObject.transform.localPosition.x
        float newLeftX = -((maxPressure) * pressure) / 100;
        rightObject.transform.localPosition = new Vector3(1 - newRightX, rightObject.transform.localPosition.y, rightObject.transform.localPosition.z);
        leftObject.transform.localPosition = new Vector3(-1 - newLeftX, leftObject.transform.localPosition.y, leftObject.transform.localPosition.z);


        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>() as BoxCollider;
        boxCollider.size = new Vector3((rightObject.transform.localPosition.x) * 2, boxCollider.size.y, boxCollider.size.z);

        float animationTime = ((handAnimation["Squeeze"].length) / 100) * (squeezePressure * 100);
        handAnimation["Squeeze"].time = handAnimation["Squeeze"].length - animationTime;

        if (handAnimation.clip.name == "Squeeze" && rightObject.transform.localPosition.x >= maxPressure)
            handAnimation.Play("Shake");
        else if (rightObject.transform.localPosition.x < maxPressure)
            handAnimation.Play("Squeeze");


        //Debug.Log(leftObject.transform.localPosition.x);
    }
    private void Squeezing(float openPressure,float closePressure)
    {
        if (rightObject != null && leftObject != null)
        {
            squeezePressure -= (openPressure * Time.deltaTime);
            squeezePressure += (closePressure * Time.deltaTime);
            if (squeezePressure > maxPressure)
                squeezePressure = maxPressure;
            else if (squeezePressure < minPressure)
                squeezePressure = minPressure;

            VisualSqueeze(squeezePressure * 100);

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

            RaycastHit[] hit;
            if (targetMonster != null)
            {
                //BlenderCatch blender = GameObject.FindObjectOfType<BlenderCatch>();
                Defendable defendAble = GameObject.FindObjectOfType<Defendable>();
                //Vector3 castPosition = new Vector3(transform.position.x, transform.position.y + (EUtils.GetObjectCollUnitSize(gameObject).y), transform.position.z);
                hit = Physics.RaycastAll(targetMonster.transform.position, -Vector3.up /* * 0.5f*/, float.MaxValue);
                //Debug.DrawRay(castPosition, transform.forward * attackDistance, Color.red);
               // Debug.Log(hit.Length);
                for (int i = 0; i < hit.Length; i++)
                {
                    Defendable blenderCatch = hit[i].collider.GetComponent<Defendable>();
                    if(blenderCatch != null)
                    {
                        targetMonster = null;
                        return;
                    }
                }
                monster = targetMonster.GetComponent<Monster>();
            }

            if (AutoMoveX)
            {
                if (monster != null)
                {
                    Ray monsterRay = new Ray(monster.transform.position,-Vector3.up);
                    RaycastHit monsterHit;
                    
                    if (!monster.isInBlender && !monster.isStunned)
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

                    //Vector3.Distance(transform.position, monster.transform.position) < 1)
                    //if (!monster.isInBlender && transform.position.x == monster.transform.position.x)

                    if (!monster.isInBlender && !monster.isStunned && Mathf.Abs(transform.position.x - monster.transform.position.x) < 1)
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
                    if (!monster.isInBlender && !monster.isStunned &&  Vector3.Distance(transform.position, monster.transform.position) < 1)
                    {
                        VisualSqueeze(monster.minPressure + 4);
                    }
                }
            }
        }
        else if((AutoMoveX || AutoMoveY )&& isHoldingObject)
        {
            BlenderCatch blender = GameObject.FindObjectOfType<BlenderCatch>();//EUtils.GetNearestObject(waveSpawner.SpawnedMonsters, transform.position);
            if (AutoMoveX)
            {
                //if (blender != null && /*transform.position.y == blender.transform.position.y + 7.50f*/)
                if (blender != null && Mathf.Abs(transform.position.y - blender.transform.position.y) > 5.50f)
                {
                    Vector3 targetPosition = new Vector3(blender.transform.position.x, transform.position.y, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);
                }
            }
            if (AutoMoveY)
            {
                if (blender != null)
                {
                    Vector3 targetPosition = new Vector3(transform.position.x, blender.transform.position.y + 5.75f, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10 * Time.deltaTime);
                }
            }
            if (AutoGrab)
            {
                if (blender != null)
                {
                     //if (transform.position.y == blender.transform.position.y + 7.50f && Mathf.Abs(transform.position.x - blender.transform.position.x) < 1)
                    if (Mathf.Abs(transform.position.y - blender.transform.position.y) > 5.50f && Mathf.Abs(transform.position.x - blender.transform.position.x) < 0.5f)
                    {
                        VisualSqueeze(1);
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
                    monster.transform.localPosition = monster.pickUpHandPosition;

                    isHoldingObject = true;
                    holdingObject = monster;
                }
            }
        }
    }
}