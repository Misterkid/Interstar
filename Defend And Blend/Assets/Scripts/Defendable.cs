using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Defendable : MonoBehaviour 
{   
    //Testing some shizzle
    /// <summary>
    /// The players current health
    /// </summary>
    private float currentHealth;
    private float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            HandleHealth();
        }
    }
    /// <summary>
    /// The player's max health
    /// </summary>
    public float maxHealth;

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
    /// The minimum value of the health's x pos
    /// </summary>
    private float zeroHealthXPos;

    /// <summary>
    /// The max value of the health's x pos
    /// </summary>
    private float fullHealthXPos;

    private float currentXPos;


	// Use this for initialization
	void Start () 
    {
        //Caches the the max value of the xPos is the start position (12)			
        if (healthTransform != null)
        {
            fullHealthXPos = healthTransform.localPosition.x;
           
            zeroHealthXPos = healthTransform.localPosition.x - healthTransform.rect.width;
            Debug.Log(zeroHealthXPos + "< ZeroHealth Pos");
           
            currentHealth = maxHealth;
        }
	}
	
    private void HandleHealth()
    {
        healthText.text = currentHealth + "%";
        
        
        currentXPos = (currentHealth / 100) * zeroHealthXPos;
        currentXPos = zeroHealthXPos - currentXPos;

        healthTransform.localPosition = new Vector3(currentXPos, 0, 0);

    


        if (currentHealth > maxHealth / 2) // I have more than 50% health.
        {
            visualHealth.color = new Color32((byte)handleHealthColor(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
        }
        else // I have less than 50% health. 
        {
            visualHealth.color = new Color32(255, (byte)handleHealthColor(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
        }
    }

    public void DoDamage(float pain)
    {
        if (currentHealth > 0)
        {
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
    public float handleHealthColor(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }


    
}
