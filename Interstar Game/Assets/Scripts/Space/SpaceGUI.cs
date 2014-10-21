using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpaceGUI : MonoBehaviour 
{
    public Text speedText;
    public PlayerShip playerShip;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        speedText.text = "Speed: " + Mathf.Floor(MetersToQMeters(playerShip.speed)) + " QMeters/" + Mathf.Floor(MetersToQMeters(playerShip.maxSpeed)) + "QMeters";
	}
    private float MetersToQMeters(float meters)
    {
        return meters * 7.859f;
    }
}
