﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Defendable : MonoBehaviour 
{   
    //Testing some shizzle
    public ParticleSystem blenderSmoke33;
    public ParticleSystem blenderSmoke66;
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
    /// The healt's transform rectangle
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


    /// <summary>
    /// The Blender can hold several points.
    /// If you reach this amount of points, the blender is 'full'.
    /// It means you can't drop any more fruit in it. 
    /// </summary>
    private float blenderFillPoints;
    private float BlenderFillPoints
    {
        get { return blenderFillPoints; }
        set
        {
            blenderFillPoints = value;
            HandleBlenderFilling();
            GameValues.BlenderFilledPoints += 1;
        }
    }
    /// <summary>
    /// The fillingBar transform Rectangle
    /// </summary>
    public RectTransform fillingBarRect;
    /// <summary>
    /// The Blender can be filled untill it reaches this float.
    /// </summary>
    public float fillingBarCanHoldPoints;
    /// <summary>
    /// The current points in the fillingBar (in Text)
    /// </summary>
    public Text fillingBarPointsText;
    /// <summary>
    /// The max value of Fillbar.Y
    /// </summary>
    private float fillingBarMaxY;
    /// <summary>
    /// The current value of the Fillbar.Y
    /// </summary>
    private float fillingBarCurrentY;
    /// <summary>
    /// The minimum value of the Fillbar.Y
    /// </summary>
    private float fillingBarEmptyY;
    /// <summary>
    /// The health's image, this is used for color changing
    /// </summary>
    public Image fillingBarColorImage;




	// Use this for initialization
	void Start () 
    {
        //Caches the the max value of the xPos is the start position (12)			
        if (healthTransform != null)
        {
            fullHealthXPos = healthTransform.localPosition.x;
           
            zeroHealthXPos = healthTransform.localPosition.x - healthTransform.rect.width;

            currentHealth = maxHealth;
        }

        //Caches the the max value of the xPos is the start position (12)			
        if (fillingBarRect != null)
        {
            fillingBarMaxY = fillingBarRect.localPosition.x;

            fillingBarEmptyY = fillingBarRect.localPosition.x - fillingBarRect.rect.width;

            fillingBarCurrentY = fillingBarCanHoldPoints;

             GameValues.BlenderFilledPoints += 1;
        }
       
	}
	
    private void HandleHealth()
    {
        healthText.text = currentHealth + "%";
        currentXPos = (currentHealth / 100) * zeroHealthXPos;
        currentXPos = zeroHealthXPos - currentXPos;

        healthTransform.localPosition = new Vector3(currentXPos, 0, 0);

        
        //public float handleHealthColor(float x, float in_min, float in_max, float out_min, float out_max)
        if (currentHealth > maxHealth / 2) // I have more than 50% health.
        {
            visualHealth.color = new Color32((byte)handleHealthColor(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
        }
        else // I have less than 50% health. 
        {
            visualHealth.color = new Color32(255, (byte)handleHealthColor(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
        }
    }

    private void HandleBlenderFilling()
    {
        fillingBarPointsText.text = blenderFillPoints + "%";
        fillingBarCurrentY = (currentHealth / 100) * fillingBarEmptyY;
        fillingBarCurrentY = fillingBarEmptyY - fillingBarCurrentY;

        fillingBarRect.localPosition = new Vector3(fillingBarCurrentY, 0, 0);

        if (blenderFillPoints > fillingBarCanHoldPoints / 2) // The Bar is > 50%!
        {
            fillingBarColorImage.color = new Color((byte)handleFillingBarColor(blenderFillPoints, fillingBarMaxY / 2, fillingBarMaxY, 255, 0), 255, 0, 255);
        }
        else
        {
            fillingBarColorImage.color = new Color(255, (byte)handleFillingBarColor(blenderFillPoints, 0, fillingBarMaxY / 2, 0, 255), 0, 255);
        }
    }

    public void DoDamage(float pain)
    {
        if (currentHealth > 0)
        {
            CurrentHealth -= pain;
        }
        else
        {
            GameOver gameOver = GameObject.FindObjectOfType<GameOver>();
            gameOver.DieGame();
        }

        if (currentHealth < 33 && currentHealth > 0)
        {
            if (!blenderSmoke33.gameObject.active)
                blenderSmoke33.gameObject.SetActive(true);
        }
        else if(currentHealth < 66 && currentHealth > 33)
        {
            if (!blenderSmoke66.gameObject.active)
                blenderSmoke66.gameObject.SetActive(true);
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
    /// <summary>
    /// This method maps a range of number into another range
    /// </summary>
    /// <param name="x">The value to evaluate</param>
    /// <param name="in_min">The minimum value of the evaluated variable</param>
    /// <param name="in_max">The maximum value of the evaluated variable</param>
    /// <param name="out_min">The minum number we want to map to</param>
    /// <param name="out_max">The maximum number we want to map to</param>
    /// <returns></returns>
    public float handleFillingBarColor(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    
}
