using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//IMPLEMENTED HEALTHBAR TUTORIAL:
// REFERENCE BELOW!
// http://www.youtube.com/watch?v=NgftVg3idB4


public class Defendable : MonoBehaviour 
{
    public float health = 100;
    
    //Testing some shizzle
    /// <summary>
    /// The players current health
    /// </summary>
    private float currentHealth;

    /// <summary>
    /// The player's max health
    /// </summary>
    public int maxHealth;

    /// <summary>
    /// The healt's transform, this is used for moving the object
    /// </summary>
    public RectTransform healthTransform;

    /// <summary>
    /// The health text
    /// </summary>
    public Text healthText;

    /// <summary>
    /// The health's image, this is used for color changing
    /// </summary>
    public Image visualHealth;

    /// <summary>
    /// The health's y pos
    /// </summary>
    private float cachedY;

    /// <summary>
    /// The minimum value of the health's x pos
    /// </summary>
    private float minXValue;

    /// <summary>
    /// The max value of the health's x pos
    /// </summary>
    private float maxXValue;

    /// <summary>
    /// The current xValue of the health
    /// </summary>
    private float currentXValue;

    private float CurrentHealth
    {
        get { return currentHealth; }
        set 
        { 
            currentHealth = value;
            HandleHealth();
        }
    }

    //End of Testing some hpbars.

	// Use this for initialization
	void Start () 
    {
        //Caches the healthbars' startposition. (5.926359)
        cachedY = healthTransform.position.y;

        //Caches the the max value of the xPos is the start position (12)			
        maxXValue = healthTransform.position.x;
        //The minValue of the xPos is startPos - the width of the bar
        // minXvalue = 12 - 250 = -238.			
        minXValue = healthTransform.position.x - healthTransform.rect.width;

        //In the start, the currentHealth is always the maxHealth (100)
        currentHealth = maxHealth;

        Debug.Log("("+currentHealth +"x" + 0 +") x (" + maxXValue +"-" + minXValue + ") / (" +maxHealth +"-" +0 +") "+minXValue);
	}
	
    private void HandleHealth()
    {
        healthText.text = "Health: " + currentHealth;

        currentXValue = Map(currentHealth, 0, maxHealth, minXValue, maxXValue);

        //private float MapValues(float x, float in_min, float in_max, float out_min, float out_max)
        Debug.Log("("+currentHealth +"x" + 0 +") x (" + maxXValue +"-" + minXValue + ") / (" +maxHealth +"-" +0 +") "+minXValue);
        Debug.Log (currentXValue);

        Debug.Log(Map(currentHealth, 0, maxHealth, minXValue, maxXValue));

        healthTransform.position = new Vector3(currentXValue, cachedY);

        if (currentHealth > maxHealth / 2) // I have more than 50% health.
        {
            visualHealth.color = new Color32((byte)Map(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
        }
        else // I have less than 50% health. 
        {
            visualHealth.color = new Color32(255, (byte)Map(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
        }
    }

    

    public void DoDamage(float pain)
    {
        if (currentHealth > 0)
        {
            
            health -= pain;
            //CurrentHealth -= 2;

            CurrentHealth -= pain;
        }
    }
    /// <summary>
    /// This method maps a range of number into another range
    /// </summary>
    /// <param name="x">The value to evaluate</param>
    /// <param name="in_min">The minimum value of the evaluated variable</param>
    /// <param name="in_max">The maximum value of the evaluated variable</param>
    /// <param name="out_min">The minum number we want to map to</param>
    /// <param name="out_max">The maximum number we want to map to</param>
    /// <returns></returns>
    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
