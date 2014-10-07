using UnityEngine;
using System.Collections;

public class Grabber : MonoBehaviour 
{
    //It Works! Hello Github!
    public PickUps holdingObject;//holding object
    //private PickUps targetObject; //target object
    public bool isDown = false;// is the grabber down
   // public bool isGoingDown = false;
    public bool isUp = true;//are we up??
    public bool isHoldingObject = false;// Are we holding a object -.-
    public int squeezePower = 0;//pppppppsqueeze power!
    private float startHeight;//Height we spawn in.
	// Use this for initialization
	void Start () 
    {
	    startHeight = transform.position.y;//Set start height
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.DrawRay(transform.position, -transform.up);
	}
    //Move the grabber Up and down using pressure
    public void MovePressure(float speed)
    {
        RaycastHit hit;//What do we hit?
        Ray ray = new Ray(transform.position, -transform.up);//Ray down from this object.
        if (Physics.Raycast(ray, out hit, 50))//Raycast with a distance of 50. We also get to know what we hit
        {
            PickUps pickUp = hit.collider.gameObject.GetComponent<PickUps>();//Do we hit a pickup?
            //The target height. Till what height should we go down ?
            float targetHeight = hit.point.y + 0.5f; //(EUtils.GetObjectUnitSize(this.gameObject).y / 2);
            //are we above the target height and is the presure below 0
            if ((transform.position.y > targetHeight && speed < 0))
            {
                if(pickUp != null)//Is there an object we can pickup?
                {
                    transform.Translate(0, speed * Time.deltaTime, 0);//Go down
                    isUp = false;
                }
                else
                {
                   // isDown = true;//No object? We can't go down (putting this on true makes it able to go up)
                }
            }
            //are we below the start position and is the pressure above 0?
            if ((transform.position.y < startHeight && speed > 0))
            {
               // Debug.Log(speed);
                transform.Translate(0, speed * Time.deltaTime, 0);//go up.
                isDown = false;
            }
            //transform.Translate(0, speed * Time.deltaTime, 0);
            float distanceDown = Mathf.Abs(transform.position.y - targetHeight);
            float distanceUp = Mathf.Abs(transform.position.y - startHeight);
            if (distanceDown < 0.1f)
            {
                if (pickUp != null)
                {
                    if (pickUp.isGrounded)
                    {
                        //Grab();
                        //Grab(pickUp);
                        isDown = true;
                    }
                }
            }
            else if (distanceUp < 0.1f)
            {
               // isDown = false;
                isUp = true;
            }
        }
    }
    //To do fix
    public void MoveDown(float speed, bool autoGrab = true)
    {
        if (!isDown && !isHoldingObject)//We can't go down if we are down?
        {
            RaycastHit hit;//What do we hit?
            Ray ray = new Ray(transform.position, -transform.up);//Ray down from this object.
            if (Physics.Raycast(ray, out hit, 50))//Raycast with a distance of 50. We also get to know what we hit
            {
                PickUps pickUp = hit.collider.gameObject.GetComponent<PickUps>();//Do we hit a pickup?
                if (pickUp != null)//yes we do then
                {
                    float targetHeight = hit.point.y + 0.5f;
                    if (pickUp.isGrounded)
                    {
                        Vector3 targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);
                        if (transform.position != targetPosition)
                        {
                            isUp = false;
                            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                        }
                        else
                        {
                            isDown = true;
                            if (autoGrab)
                            {
                                Grab();
                            }
                        }
                    }
                }
                /*
                else
                {
                    if(autoGrab)
                        isDown = true;
                }
                 */
            }
        }
    }
    public void MoveUp(float speed, bool auto = false)
    {
        //if (isDown)
        //{
            Vector3 targetPosition = new Vector3(transform.position.x, startHeight, transform.position.z);
            if (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                isUp = true;
                isDown = false;
            }
        //}
    }
    public void Squeeze(int power)//please
    {
        squeezePower = power;
        if (holdingObject == null)
        {
            Grab();
            isDown = false;
        }
        else
        {
            if (power < holdingObject.minPressure)
            {
                LetGo();
            }
            else if (power > holdingObject.maxPressure)
            {
                Destroy(holdingObject.gameObject);
                holdingObject = null;
                isHoldingObject = false;
            }
        }
    }
    private void Grab()
    {
        if (holdingObject == null && isDown)
        {
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 1f))
            {
                if (hit.collider.GetComponent<PickUps>() != null)
                {
                    holdingObject = hit.collider.GetComponent<PickUps>();
                    isHoldingObject = true;
                    float addedHeight = (EUtils.GetObjectUnitSize(holdingObject.gameObject).y / 2);
                    holdingObject.transform.position = new Vector3(transform.position.x, transform.position.y - addedHeight, transform.position.z);
                    holdingObject.transform.parent = this.transform;
                    holdingObject.rigidbody.isKinematic = true;
                }
            }
        }
    }
    /*
    private void Grab(PickUps pickUp)
    {
        holdingObject = pickUp;
        isHoldingObject = true;
        float addedHeight = (EUtils.GetObjectUnitSize(holdingObject.gameObject).y / 2);
        holdingObject.transform.position = new Vector3(transform.position.x, transform.position.y - addedHeight, transform.position.z);
        holdingObject.transform.parent = this.transform;
        holdingObject.rigidbody.isKinematic = true;
    }
     */ 
    public void LetGo()
    {
        if (holdingObject != null)
        {
            holdingObject.isGrounded = false;
            holdingObject.rigidbody.isKinematic = false;
            holdingObject.transform.parent = null;
            holdingObject = null;
            isHoldingObject = false;
        }
    }
    public bool CatcherCheck()
    {
        if(isHoldingObject)
        {
            RaycastHit hit;//What do we hit?
            //holdingObject.transform
            Ray ray = new Ray(holdingObject.transform.position, -transform.up);//Ray down from this object.
            if (Physics.Raycast(ray, out hit, 50))//Raycast with a distance of 50. We also get to know what we hit
            {
                //Debug.Log(hit.collider);
                CatchTrigger catcher = hit.collider.GetComponent<CatchTrigger>();
                if (catcher != null)
                {
                    //cheetsy doodle
                    //if (!isDown)
                        //isDown = true;

                    return true;
                }
            }
        }
        return false;
    }
}
