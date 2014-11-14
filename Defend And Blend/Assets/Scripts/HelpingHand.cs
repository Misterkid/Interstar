using UnityEngine;
using System.Collections;
public class HelpingHand : MonoBehaviour 
{
    public GameObject rightObject;
    public GameObject leftObject;
    //public int[] monstersList;
    public float maxHeight;
    public float minHeight;
    public float maxPressure = 1;
    public float minPressure = 0;
   // private float rightObjectStart;
    //private float leftObjectStart;
    private bool isHoldingObject = false;
    private Monster holdingObject;
    private float squeezePressure;
	// Use this for initialization
	void Start () 
    {
        //rightObjectStart = rightObject.transform.localPosition.x;
       // leftObjectStart = leftObject.transform.localPosition.x;
        //Physics.IgnoreCollision(gameObject.collider, rightObject.collider);
        //Physics.IgnoreCollision(gameObject.collider, leftObject.collider);
        //Physics.IgnoreCollision(gameObject.collider, gameObject.collider);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    //Controls
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
            //squeezePressure = rightObjectStart - (rightObjectStart * Input.GetAxis("RTrigger"));//Get the trigger / pressure value.
            float openPressure = Input.GetAxis("RTrigger");
            float closePressure = Input.GetAxis("LTrigger");
            if (openPressure > 0 && rightObject.transform.localPosition.x < maxPressure)
            {
                rightObject.transform.localPosition = new Vector3(rightObject.transform.localPosition.x + (openPressure * Time.deltaTime), rightObject.transform.localPosition.y, rightObject.transform.localPosition.z);
                leftObject.transform.localPosition = new Vector3(leftObject.transform.localPosition.x - (openPressure * Time.deltaTime), leftObject.transform.localPosition.y, leftObject.transform.localPosition.z);
                /*
                if(rightObject.transform.localPosition.x > rightObjectStart)
                    rightObject.transform.localPosition = new Vector3(rightObjectStart, rightObject.transform.localPosition.y, rightObject.transform.localPosition.z);

                if (leftObject.transform.localPosition.x < leftObjectStart)
                    leftObject.transform.localPosition = new Vector3(leftObjectStart, leftObject.transform.localPosition.y, leftObject.transform.localPosition.z);
                */
             }
            else if(closePressure > 0 && rightObject.transform.localPosition.x > minPressure)
            {
                rightObject.transform.localPosition = new Vector3(rightObject.transform.localPosition.x - (closePressure * Time.deltaTime), rightObject.transform.localPosition.y, rightObject.transform.localPosition.z);
                leftObject.transform.localPosition = new Vector3(leftObject.transform.localPosition.x + (closePressure * Time.deltaTime), leftObject.transform.localPosition.y, leftObject.transform.localPosition.z);
                
            }
           // Debug.Log(squeezePressure);
            //Close

            //rightObject.transform.localPosition = new Vector3(squeezePressure, rightObject.transform.localPosition.y, rightObject.transform.localPosition.z);
            //leftObject.transform.localPosition = new Vector3(-squeezePressure, leftObject.transform.localPosition.y, leftObject.transform.localPosition.z);
            BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>() as BoxCollider;
            boxCollider.size = new Vector3( (rightObject.transform.localPosition.x) * 2, boxCollider.size.y, boxCollider.size.z);
            squeezePressure = maxPressure - Vector3.Distance(rightObject.transform.localPosition, leftObject.transform.localPosition);
        }

        if(isHoldingObject)
        {
            //if (holdingObject != null)
            //{
                //Debug.Log(Input.GetAxis("RTrigger") * 100 + ":" + holdingObject.minPressure);
                if (holdingObject != null && squeezePressure * 100 < holdingObject.minPressure)
                {
                    holdingObject.transform.parent = null;
                    //Debug.Log(holdingObject.transform.localPosition);
                   // Debug.Log(Input.GetAxis("RTrigger") * 100);
                    isHoldingObject = false;
                    holdingObject.LetGo();
                    holdingObject.Stun(1);
                    holdingObject = null;
                }
                if (holdingObject != null && squeezePressure * 100 > holdingObject.maxPressure)
                {
                    isHoldingObject = false;
                    holdingObject.Die();
                    GameValues.SCORE--;
                    holdingObject = null;
                }
            //}
        }
	}
    //private void OnTriggerEnter(Collider other)
    private void OnTriggerStay(Collider other)
    {
       // Debug.Log(Input.GetAxis("RTrigger") * 100);
        if (!isHoldingObject)
        {
            //Debug.Log(squeezePressure + ":" + squeezePressure * 100);
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                //Debug.Log(Input.GetAxis("RTrigger") * 100 + ":" + monster.minPressure);
                if (squeezePressure * 100 > monster.minPressure)
                {
                    monster.transform.parent = this.transform;
                    monster.Hold();
                    monster.transform.localPosition = Vector3.zero;

                    Banana banana = other.GetComponent<Banana>();
                    if (banana != null)
                        banana.DropPeel();

                    isHoldingObject = true;
                    holdingObject = monster;
                }
            }
        }
    }
}