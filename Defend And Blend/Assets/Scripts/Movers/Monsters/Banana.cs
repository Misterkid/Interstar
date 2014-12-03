using UnityEngine;
using System.Collections;

public class Banana : Monster 
{
    public BananaPeel bananaPeel;
    public int peelCount = 1;
    // Use this for initialization
    /*
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    */
    //drop a banana peel.
    public void DropPeel()
    {
        //ToDo
        if (peelCount > 0)
        {
            Vector3 position = transform.position;//Current position
            RaycastHit hit;//For the raycast hit result
            //Raycast down from object position
            if(Physics.Raycast(transform.position,-transform.up,out hit,10))
            {

                GameObject clone = GameObject.Instantiate(bananaPeel.gameObject, position, transform.rotation) as GameObject;//Drop peel
                Physics.IgnoreCollision(clone.collider, collider);
                position.y = hit.point.y + (EUtils.GetObjectCollUnitSize(clone).y / 2);//Change y position to the hitting y position.
                clone.transform.position = position;
                Debug.Log((EUtils.GetObjectCollUnitSize(clone).y / 2));
            }

           //Clone prefeb object and place it in the game!
            peelCount--;//one less peel to drop
        }
    }
    /*
    protected override void OnCollisionEnter(Collision collision)
    {
        //Do base collision crap
        base.OnCollisionEnter(collision);
    }
     */ 
}
