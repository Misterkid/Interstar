using UnityEngine;
using System.Collections;
public class HelpingHand : MonoBehaviour 
{
    public GameObject rightObject;
    public GameObject leftObject;
    //public int[] monstersList;
    public float maxHeight;
    public float minHeight;
    private float rightObjectStart;
    private float leftObjectStart;
    private bool isHoldingObject = false;
    private Monster holdingObject;
    private float squeezePressure;
	// Use this for initialization
	void Start () 
    {
        rightObjectStart = rightObject.transform.localPosition.x;
        leftObjectStart = leftObject.transform.localPosition.x;
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
            squeezePressure = rightObjectStart - (rightObjectStart * Input.GetAxis("RTrigger"));//Get the trigger / pressure value.
            rightObject.transform.localPosition = new Vector3(squeezePressure, rightObject.transform.localPosition.y, rightObject.transform.localPosition.z);
            leftObject.transform.localPosition = new Vector3(-squeezePressure, leftObject.transform.localPosition.y, leftObject.transform.localPosition.z);
           // collider.bounds.size = new Vector3(collider.bounds.size.x, collider.bounds.size.y, collider.bounds.size.z);
            BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>() as BoxCollider;
            boxCollider.size = new Vector3( (rightObject.transform.localPosition.x) * 2, boxCollider.size.y, boxCollider.size.z);
        }

        if(isHoldingObject)
        {
            //if (holdingObject != null)
            //{
                //Debug.Log(Input.GetAxis("RTrigger") * 100 + ":" + holdingObject.minPressure);
                if (holdingObject != null &&  Input.GetAxis("RTrigger") * 100 < holdingObject.minPressure)
                {
                    holdingObject.transform.parent = null;
                    //Debug.Log(holdingObject.transform.localPosition);
                   // Debug.Log(Input.GetAxis("RTrigger") * 100);
                    isHoldingObject = false;
                    holdingObject.LetGo();
                    holdingObject.Stun(1);
                    holdingObject = null;
                }
                if (holdingObject != null && Input.GetAxis("RTrigger") * 100 > holdingObject.maxPressure)
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
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                //Debug.Log(Input.GetAxis("RTrigger") * 100 + ":" + monster.minPressure);
                if (Input.GetAxis("RTrigger") * 100 > monster.minPressure)
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