using UnityEngine;
using System.Collections;

public class Strawberry : Monster 
{

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
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
