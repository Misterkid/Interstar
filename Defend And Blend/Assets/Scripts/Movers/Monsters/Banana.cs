using UnityEngine;
using System.Collections;

public class Banana : Monster 
{
    public BananaPeel bananaPeel;
    public int peelCount = 1;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
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
                position.y = hit.point.y;//Change y position to the hitting y position.
            }

           //Clone prefeb object and place it in the game!
            GameObject clone = GameObject.Instantiate(bananaPeel, position, transform.rotation) as GameObject;//Drop peel
            peelCount--;//one less peel to drop
        }
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        //Do base collision crap
        base.OnCollisionEnter(collision);
    }
}
