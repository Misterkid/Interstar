using UnityEngine;
using System.Collections;
public class MovementRail : MonoBehaviour 
{
    public int minMaxAddition = -5;//add or subtract from the parent object length.

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

	}
    //Jupe looks the same as MovementMount.Move but not exactly. Maybe this will be diffrent in the future ? Who knows.
    public void Move(float speed,bool auto = false,bool isHoldingObject = false)
    {
        float diffrenceZ = transform.parent.position.z + (EUtils.GetObjectUnitSize(transform.parent.gameObject).z / 2);//min and max WORLD position.
       // Debug.Log(diffrenceZ + ":" + transform.position.z);
        if (!auto)
        {
            //Just read it. It is to see if the object is out of the moving area!
            if ((transform.position.z < (diffrenceZ + minMaxAddition) && speed > 0) || (transform.position.z > -(diffrenceZ + minMaxAddition) && speed < 0))
                transform.Translate(0, 0, speed * Time.deltaTime);//Moves object forward and backwards.
        }
        else
        {
            //Auto
            if (isHoldingObject)
            {
                MoveToCatcher(speed);
            }
            else
            {
                GameObject nearestObject = EUtils.GetNearestObjectOfType<PickUps>(transform.position);
                if (nearestObject != null)
                {
                    Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, nearestObject.transform.position.z);
                    if (transform.position != targetPosition)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                    }
                }
                else
                {
                    MoveToCatcher(speed);
                }
            }
        }
    }
    private void MoveToCatcher(float speed)
    {
        GameObject catcherObject = EUtils.GetNearestObjectOfType<CatchTrigger>(transform.position);
        if (catcherObject != null)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, catcherObject.transform.position.z);
            if (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
        }
    }
}
