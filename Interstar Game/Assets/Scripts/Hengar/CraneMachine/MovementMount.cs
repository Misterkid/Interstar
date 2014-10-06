using UnityEngine;
using System.Collections;

public class MovementMount : MonoBehaviour 
{
    public int minMaxAddition = -5;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    //Jupe looks the same as MovementRail.Move but not exactly. Maybe this will be diffrent in the future ? Who knows.
    public void Move(float speed, bool auto = false, bool isHoldingObject = false)
    {
        float diffrenceX = transform.parent.position.x + (EUtils.GetObjectUnitSize(transform.parent.gameObject).x / 2);//min and max WORLD position.
        if (!auto)
        {
            //For some reason localposition works better here.
            //Just read it. It is to see if the object is out of the moving area!
            if ((transform.localPosition.x < (diffrenceX + minMaxAddition) && speed > 0) || (transform.localPosition.x > -(diffrenceX + minMaxAddition) && speed < 0))
                transform.Translate(speed * Time.deltaTime, 0, 0);//Move object from left to right...
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
                    Vector3 targetPosition = new Vector3(nearestObject.transform.position.x, transform.position.y, transform.position.z);
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
            Vector3 targetPosition = new Vector3(catcherObject.transform.position.x, transform.position.y, transform.position.z);
            if (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
        }
    }
}
