using UnityEngine;
using System.Collections;

public class Banana : Monster 
{
    public BananaPeel bananaPeel;
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
    private void DropPeel()
    {
        //ToDo
        GameObject.Instantiate(bananaPeel);//Drop peel
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
