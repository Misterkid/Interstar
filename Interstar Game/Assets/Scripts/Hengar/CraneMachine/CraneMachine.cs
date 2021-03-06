﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CraneMachine : MonoBehaviour 
{
    public float speed;//movement speed of all the object in this crane!
    //They speak for themself.
    public MovementRail movementRail;
    public MovementMount movementMount;
    public Grabber grabber;
    public Text pressureText;
    public Text nameText;
    public Text minMaxPressureText;
    //Settings. Many booleans for diffrent way's to move.
    [System.Serializable] 
    public struct MovementSettings
    {
        //Move by pressure. There can only be one true! I should make a check for that later.
        public bool movementRailPressure;
        public bool movementMountPressure;
        public bool grabberPressure;
        //Move the objects Automatic to the nearest grabable object if true.
        public bool movementRailAuto;
        public bool movementMountAuto;
        public bool grabberAuto;
        public bool grabberSqueeze;
        //All off uses the xbox sticks/Buttons
    }
    public  MovementSettings movementSettings;
    //public bool a
	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        pressureText.text = "Pressure: " + grabber.squeezePower;
        if (grabber.holdingObject != null)
        {
            nameText.text = "Name: " + grabber.holdingObject.objectName;
            minMaxPressureText.text = "Min Pressure: " + grabber.holdingObject.minPressure + " Max: " + grabber.holdingObject.maxPressure;
        }
        else
        {
            minMaxPressureText.text = "Min Pressure: -"  + " Max: -" ;
            nameText.text = "Name: -";
        }
	}
    void FixedUpdate()
    {
        //Squeeze pressure /Trigger (-speed/2) and (speed/2)
        float squeezePressure = 0;
        //Lets not use this when we don't need it! :O
        if (movementSettings.movementRailPressure || movementSettings.grabberPressure || movementSettings.movementMountPressure)
        {

            squeezePressure = (speed * Input.GetAxis("RTrigger"));//Get the trigger / pressure value.
            squeezePressure = (speed / 2) - squeezePressure;//convert to speed/2 minus the pressure making it (-speed/2) and (speed/2)
            squeezePressure *= -1;//Flip it!
        }

        //MovementRail
        if (!movementSettings.movementRailAuto)
        {
            if (!grabber.isDown && grabber.isUp)
            {
                if (!movementSettings.movementRailPressure)
                    movementRail.Move(Input.GetAxis("Vertical") * speed);//move the movementRail forward and backwards.(The Z axis) using the Vertical Axis of an controller
                else
                    movementRail.Move(squeezePressure);//move the movementRail using pressure
            }
        }
        else
        {
            if (!grabber.isDown && grabber.isUp)
                movementRail.Move(speed, true,grabber.isHoldingObject);//Move automatic :P
        }
        //MovementMount
        if (!movementSettings.movementMountAuto)
        {
            if (!grabber.isDown && grabber.isUp)
            {
                if (!movementSettings.movementMountPressure)
                    movementMount.Move(Input.GetAxis("Horizontal") * speed);//move the movementMount left and right.(The x axis) using the Horizontal Axis of an controller
                else
                    movementMount.Move(squeezePressure);//move the movementRail using pressure
            }
        }
        else
        {
            if (!grabber.isDown && grabber.isUp)
                movementMount.Move(speed, true, grabber.isHoldingObject);
        }

        //Grabber
        if(!movementSettings.grabberAuto)
        {
            if (!movementSettings.grabberPressure)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (movementSettings.grabberSqueeze)
                    {
                        grabber.MoveDown(speed,false);
                    }
                    else
                    {
                        grabber.MoveDown(speed,true);
                    }
                }
                if (Input.GetButton("Fire2"))
                {
                    grabber.MoveUp(speed);
                }
               
                if (movementSettings.grabberSqueeze)
                {
                    int squeezePower = Mathf.FloorToInt(Input.GetAxis("RTrigger") * 100);
                    grabber.Squeeze(squeezePower);
                }
                else
                {
                    if (grabber.CatcherCheck())
                    {
                        grabber.LetGo();
                    }
                }

            }
            else
            {
                grabber.MovePressure(squeezePressure);
                if (grabber.CatcherCheck())
                {
                    grabber.LetGo();
                }
            }
        }
        else
        {
            if (!grabber.isDown )
            {
                if (movementSettings.grabberSqueeze)
                {
                    grabber.MoveDown(speed, false);
                }
                else
                {
                    grabber.MoveDown(speed, true);
                }
            }
            else
            {
                if (movementSettings.grabberSqueeze)
                {
                    //Move up when there is a target or when we are holding a object.
                    if(grabber.targetObject == null || grabber.isHoldingObject || (!grabber.isHoldingObject && grabber.isGoingUp) )
                        grabber.MoveUp(speed, true);
                }
                else
                {
                    grabber.MoveUp(speed, true);
                }
            }
            if (movementSettings.grabberSqueeze)
            {
                int squeezePower = Mathf.FloorToInt(Input.GetAxis("RTrigger") * 100);
                grabber.Squeeze(squeezePower);
            }
            else
            {
                if (grabber.CatcherCheck())
                {
                    grabber.LetGo();
                }
            }
        }

    }
}
