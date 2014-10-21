using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TestOptions : MonoBehaviour 
{
    public CraneMachine craneMachine;// We need the movementSettings :O
    public Toggle mountPressureToggle,
                  mountAutoToggle;
    public Toggle railPressureToggle;
    public Toggle railAutoToggle;
    public Toggle grabberPressureToggle;
    public Toggle grabberAutoToggle;
    public Toggle grabberSqueezeToggle;
	void Start () 
    {
        mountPressureToggle.isOn = craneMachine.movementSettings.movementMountPressure;
        mountAutoToggle.isOn = craneMachine.movementSettings.movementMountAuto;
        railPressureToggle.isOn = craneMachine.movementSettings.movementRailPressure;
        railAutoToggle.isOn = craneMachine.movementSettings.movementRailAuto;
        grabberPressureToggle.isOn = craneMachine.movementSettings.grabberPressure;
        grabberAutoToggle.isOn = craneMachine.movementSettings.grabberAuto;
        grabberSqueezeToggle.isOn = craneMachine.movementSettings.grabberSqueeze;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}
    public void LoadLevel(int id)
    {
        Application.LoadLevel(id);
    }
    public void Quit()
    {
        Debug.Log("User Quit");
        Application.Quit();
    }
    public void RefreshValues()
    {
        //mountPressureToggle.isOn;
        //mountAutoToggle.isOn
        //Make toggle for the booleans so it can be turned on and off.
        craneMachine.movementSettings.movementMountPressure = mountPressureToggle.isOn;
        craneMachine.movementSettings.movementMountAuto = mountAutoToggle.isOn;
        //Check if the other pressure options are turned on and if you are trying to turn this one on.
        //It would be weird if you can control 2 things with 1 pressure!
        if ((craneMachine.movementSettings.movementRailPressure || craneMachine.movementSettings.grabberPressure || craneMachine.movementSettings.grabberSqueeze) && craneMachine.movementSettings.movementMountPressure)
        {
            Debug.Log("Only one option can be used with pressure");
            craneMachine.movementSettings.movementMountPressure = false;//Set it back to false! Ha!
            mountPressureToggle.isOn = false;
        }
        //Repeat
        //railPressureToggle.isOn;
        //railAutoToggle.isOn;
        craneMachine.movementSettings.movementRailPressure = railPressureToggle.isOn;
        craneMachine.movementSettings.movementRailAuto = railAutoToggle.isOn;
        if ((craneMachine.movementSettings.movementMountPressure || craneMachine.movementSettings.grabberPressure || craneMachine.movementSettings.grabberSqueeze) && craneMachine.movementSettings.movementRailPressure)
        {
            Debug.Log("Only one option can be used with pressure");
            railPressureToggle.isOn = false;
            craneMachine.movementSettings.movementRailPressure = false;
        }
        //repeat
        //grabberPressureToggle.isOn;
        //grabberAutoToggle.isOn;
        //grabberSqueezeToggle.isOn;
        craneMachine.movementSettings.grabberPressure = grabberPressureToggle.isOn;
        craneMachine.movementSettings.grabberAuto = grabberAutoToggle.isOn;
        if ((craneMachine.movementSettings.movementMountPressure || craneMachine.movementSettings.movementRailPressure || craneMachine.movementSettings.grabberSqueeze) && craneMachine.movementSettings.grabberPressure)
        {
            Debug.Log("Only one option can be used with pressure");
            craneMachine.movementSettings.grabberPressure = false;
            grabberPressureToggle.isOn = false;
        }
        craneMachine.movementSettings.grabberSqueeze = grabberSqueezeToggle.isOn;
        if ((craneMachine.movementSettings.movementMountPressure || craneMachine.movementSettings.movementRailPressure || craneMachine.movementSettings.grabberPressure) && craneMachine.movementSettings.grabberSqueeze)
        {
            Debug.Log("Only one option can be used with pressure");
            craneMachine.movementSettings.grabberSqueeze = false;
            grabberSqueezeToggle.isOn = false;
        }
    }
}
